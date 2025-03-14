using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PoliziaMunicipale2.Models;

[Table("VERBALE")]
public partial class Verbale
{
    [Key]
    [Column("idverbale")]
    public int Idverbale { get; set; }

    [Column("idanagrafica")]
    public int? Idanagrafica { get; set; }

    [Column("idviolazione")]
    public int? Idviolazione { get; set; }

    public DateOnly DataViolazione { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? IndirizzoViolazione { get; set; }

    [Column("Nominativo_Agente")]
    [StringLength(255)]
    [Unicode(false)]
    public string? NominativoAgente { get; set; }

    public DateOnly? DataTrascrizioneVerbale { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Importo { get; set; }

    public int? DecurtamentoPunti { get; set; }

    [ForeignKey("Idanagrafica")]
    [InverseProperty("Verbales")]
    public virtual Anagrafica? IdanagraficaNavigation { get; set; }

    [ForeignKey("Idviolazione")]
    [InverseProperty("Verbales")]
    public virtual TipoViolazione? IdviolazioneNavigation { get; set; }
}
