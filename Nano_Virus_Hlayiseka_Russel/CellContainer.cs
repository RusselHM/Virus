using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nano_Virus_Hlayiseka_Russel
{
    class CellContainer
    {
        #region Properties
        private static CellContainer container = null;
        private static Cell cell = new Cell(0, 0, 0,0, CellTypes.RedBloodCell);
        private Virus virus = new Virus(cell);

        private int cycleNo = 0;
        private int numberOfDeadCells = 0;

        Random random = new Random();

        //CELL INSTANCES
        private List<Cell> redBloodCells;
        private List<Cell> whiteBloodCells;
        private List<Cell> tumourousCells;

        public static CellContainer Container
        {
            get { return container == null ? container = new CellContainer() : null; }
            set { container = value; }
        }

        public List<Cell> RedBloodCells
        {
            get { return this.redBloodCells; }
            set { this.redBloodCells = value; }
        }

        public List<Cell> WhiteBloodCells
        {
            get { return this.whiteBloodCells; }
            set { this.whiteBloodCells = value; }
        }

        public List<Cell> TumourousCells
        {
            get { return this.tumourousCells; }
            set { this.tumourousCells = value; }
        }

        public Virus Virus
        {
            get { return virus; }
            set { virus = value; }
        }
        public int NumberOfDeadCells
        {
            get
            {
                return this.numberOfDeadCells;
            }
        }

        public int CycleNo
        {
            get
            {
                return this.cycleNo;
            }
            set
            {
                cycleNo = value;
            }
        }

        #endregion

        #region Constructor
        public CellContainer()
        {
            Generate100Cells();
        }
        #endregion

        #region Methods
        public void Generate100Cells()
        {
            redBloodCells = new List<Cell>();
            whiteBloodCells = new List<Cell>();
            tumourousCells = new List<Cell>();

            for (int i = 0; i < 100; i++)
            {
                Cell cellInstance = GenerateCell(i);

                switch (cellInstance.cellType)
                {
                    case CellTypes.RedBloodCell:
                        redBloodCells.Add(cellInstance);
                        break;
                    case CellTypes.WhiteBloodCell:
                        whiteBloodCells.Add(cellInstance);
                        break;
                    case CellTypes.Tumorouscell:
                        tumourousCells.Add(cellInstance);
                        break;
                    default:
                        break;
                }
            }
        }

        public static CellContainer ContainerInstance
        {
            get
            {
                if (container == null)
                    container = new CellContainer();

                return container;
            }
        }

        public Cell GenerateCell(int cellId)
        {
            int cellTypesPercentages = random.Next(1, 100);
            int xCoord = 0;
            int yCoord = 0;
            int zCoord = 0;

            CellTypes type;

            type = cellTypesPercentages <= 5 ? CellTypes.Tumorouscell :
                   cellTypesPercentages > 5 && cellTypesPercentages <= 25 ?
                   CellTypes.WhiteBloodCell : CellTypes.RedBloodCell;

            xCoord = random.Next(1, 5001);
            yCoord = random.Next(1, 5001);
            zCoord = random.Next(1, 5001);

            return new Cell(cellId, xCoord, yCoord, zCoord, type);
        }

        public void MoveToCells()
        {
            int tomourousCellsCounter = TumourousCells.Count;
            for (int i = 0; i < tomourousCellsCounter; i++)
            {
                Cell tumorousCell = TumourousCells[i];
                Cell targetCell = null;
                if (redBloodCells.Count > 0)
                {
                    double smallestDistance = tumorousCell.CalculateCellDistance(redBloodCells[0]);
                    int smallestCellIndex = 0;

                    for (int j = 1; i < redBloodCells.Count; j++)
                    {
                        double distance = tumorousCell.CalculateCellDistance(redBloodCells[j]);
                        if (distance < smallestDistance)
                        {
                            smallestDistance = distance;
                            smallestCellIndex = j;
                        }

                    }

                    targetCell = redBloodCells[smallestCellIndex];
                    redBloodCells.RemoveAt(smallestCellIndex);

                }
                else if (whiteBloodCells.Count > 0)
                {
                    double smallestDistance = tumorousCell.CalculateCellDistance(whiteBloodCells[0]);
                    int smallestCellIndex = 0;

                    for (int j = 1; j < WhiteBloodCells.Count; j++)
                    {
                        double distance = tumorousCell.CalculateCellDistance(whiteBloodCells[j]);

                        if (distance < smallestDistance)
                        {
                            smallestDistance = distance;
                            smallestCellIndex = j;
                        }
                    }

                    targetCell = WhiteBloodCells[smallestCellIndex];

                    WhiteBloodCells.RemoveAt(smallestCellIndex);
                }

                if (targetCell != null)
                {
                    targetCell.cellType = CellTypes.Tumorouscell;

                    TumourousCells.Add(targetCell);
                }
            }

        }


        public void DestroyCellsOrNot()
        {
            if (virus.CellID < 0 || virus.Cell.cellType != CellTypes.Tumorouscell)
            {
                List<Cell> cellsWithinRange = new List<Cell>();
                for (int i = 0; i < tumourousCells.Count; i++)
                {
                    if (virus.Cell.CalculateCellDistance(tumourousCells[i]) <= 5000)
                    {
                        cellsWithinRange.Add(tumourousCells[i]);
                    }
                    else if (virus.Cell.CalculateCellDistance(redBloodCells[i]) <= 5000)
                    {
                        cellsWithinRange.Add(redBloodCells[i]);
                    }

                    if (virus.Cell.CalculateCellDistance(whiteBloodCells[i]) <= 5000)
                    {
                        cellsWithinRange.Add(whiteBloodCells[i]);
                    }


                }

                if (cellsWithinRange.Count > 0)
                {

                    int moveToIndex = random.Next(0, cellsWithinRange.Count);

                    Cell moveToCell = cellsWithinRange[moveToIndex];

                    switch (moveToCell.cellType)
                    {
                        case CellTypes.RedBloodCell:
                            virus.Cell = RedBloodCells[RedBloodCells.IndexOf(moveToCell)];
                            break;
                        case CellTypes.WhiteBloodCell:
                            virus.Cell = WhiteBloodCells[WhiteBloodCells.IndexOf(moveToCell)];
                            break;
                        case CellTypes.Tumorouscell:
                            virus.Cell = tumourousCells[tumourousCells.IndexOf(moveToCell)];
                            break;
                        default:
                            break;
                    }
                }
                else if (virus.Cell.cellType == CellTypes.Tumorouscell)
                {
                    int index = tumourousCells.IndexOf(virus.Cell);

                    tumourousCells.RemoveAt(index);
                    numberOfDeadCells++;


                    virus.CellID = -1;
                }


            }
        }
        public string Result()
        {
            int NumberOfTumourousCells = tumourousCells.Count;
            int NumberOfWhiteBloodCell = whiteBloodCells.Count;
            int NumberOfRedBloodCells = redBloodCells.Count;
            int totalCellsRemaining = NumberOfRedBloodCells + NumberOfWhiteBloodCell + NumberOfTumourousCells;

            string result = String.Format(  "Number of Tumor Cells remaining: {1}" +  '\n' +
                                            "Number of White Blood Cells remaining: {2}" + '\n' +
                                            "Number of Red Blood Cells remaining: {3}" + '\n' +
                                            "Number of Total Cells remaining: {4}" + '\n' +
                                            "Number of Tumor Cells destroyed: {5}" + '\n', NumberOfTumourousCells,
                                            NumberOfWhiteBloodCell, NumberOfRedBloodCells, totalCellsRemaining);

            return result;
        }
        #endregion
    }
}

