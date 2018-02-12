using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nano_Virus_Hlayiseka_Russel
{
    class Virus 
    {
        #region Properties
        public static int cellID;
        public static Cell cell;
        public Cell Cell
        {
            get
            {
                return cell;
            }
            set
            {
                cell = value;
            }
        }
        public int CellID
        {
            get
            {
                return cellID;
            }
            set
            {
                cellID = value;
            }
        }
        #endregion

        #region Constructor
        public Virus(Cell cell) 
        {
            this.Cell = cell;
        }
        #endregion

        #region Methods

        #endregion

    }
}
