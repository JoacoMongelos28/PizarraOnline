using System;
using System.Collections.Generic;

namespace Grupo7_Pizarra_SignalR_Data.Entidades
{
    public partial class Dibujo
    {
        public int IdDibujo { get; set; }
        public string? Dibujo1 { get; set; }
        public int? IdSala { get; set; }

        public virtual Sala? IdSalaNavigation { get; set; }
    }
}
