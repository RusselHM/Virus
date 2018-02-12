using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nano_Virus_Hlayiseka_Russel
{
     class Cell
    {
        #region Properties
        public int cellID, x, y, z;
        public CellTypes cellType;

        public int CellID
        {
            get { return this.cellID; }
            set { cellID = value; }
        }
        public int X
        {
            get { return this.x; }
            set { x = value; }

        }
        public int Y
        {
            get { return this.y; }
            set { y = value; }

        }
        public int Z
        {
            get { return this.z; }
            set { z = value; }

        }
        #endregion Properties

        #region Constructor
        public Cell(int _cellID,int _x,int _y,int _z,CellTypes _type)
        {
            this.CellID = _cellID;
            this.X = _x;
            this.Y = _x;
            this.Z = _z;
            this.cellType = _type;
        }
        #endregion Constructor

        #region Methods
        public double CalculateCellDistance(Cell cell)
        {
            double xcoord = Math.Pow(X - cell.X, 2);
            double ycoord = Math.Pow(Y - cell.Y, 2); 
            double zcoord = Math.Pow(Z - cell.Z, 2); 

            double coordSum = (xcoord + ycoord + zcoord);

            return Math.Sqrt(coordSum);

        }
        #endregion 
    }
}
