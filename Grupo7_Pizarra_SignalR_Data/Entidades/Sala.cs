using System;
using System.Collections.Generic;

namespace Grupo7_Pizarra_SignalR_Data.Entidades
{
    public partial class Sala
    {
        public Sala()
        {
            Dibujos = new HashSet<Dibujo>();
        }

        public int IdSala { get; set; }
        public string NombreSala { get; set; } = null!;

        public virtual ICollection<Dibujo> Dibujos { get; set; }
    }
}
