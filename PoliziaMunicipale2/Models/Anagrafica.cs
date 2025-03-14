using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PoliziaMunicipale2.Models;

[Table("ANAGRAFICA")]
[Index("CodFisc", Name = "UQ__ANAGRAFI__063721E11CF24004", IsUnique = true)]
public partial class Anagrafica
{
    [Key]
    [Column("idanagrafica")]
    public int Idanagrafica { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string Cognome { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string Nome { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string? Indirizzo { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? Città { get; set; }

    [Column("CAP")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Cap { get; set; }

    [Column("Cod_Fisc")]
    [StringLength(16)]
    [Unicode(false)]
    public string? CodFisc { get; set; }

    [InverseProperty("IdanagraficaNavigation")]
    public virtual ICollection<Verbale> Verbales { get; set; } = new List<Verbale>();
}
