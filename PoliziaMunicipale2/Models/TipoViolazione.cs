﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PoliziaMunicipale2.Models;

[Table("TIPO_VIOLAZIONE")]
public partial class TipoViolazione
{
    [Key]
    [Column("idviolazione")]
    public int Idviolazione { get; set; }

    [Column("descrizione")]
    [StringLength(255)]
    [Unicode(false)]
    public string Descrizione { get; set; } = null!;

    [InverseProperty("IdviolazioneNavigation")]
    public virtual ICollection<Verbale> Verbales { get; set; } = new List<Verbale>();
}
