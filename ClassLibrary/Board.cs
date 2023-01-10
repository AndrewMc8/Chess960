/***************************************************************
 * File: Board.cs
 * Created By: Justin Grindal		Date: 27 June, 2013
 * Description: The main chess board. Board contain the chess cell
 * which will contain the chess pieces. Board also contains the methods
 * to get and set the user moves.
 ***************************************************************/

using System;
using System.Collections;
using System.Xml;

namespace ChessLibrary
{
    /// <summary>
    /// he main chess board. Board contain the chess cell
    /// which will contain the chess pieces. Board also contains the methods
    /// to get and set the user moves.
    /// </summary>
    [Serializable]
    public class Board
    {
        private Side m_WhiteSide, m_BlackSide;  // Chess board site object 
        private Cells m_cells;  // collection of cells in the board

        public Board()
        {
            m_WhiteSide = new Side(Side.SideType.White);	// Makde white side
            m_BlackSide = new Side(Side.SideType.Black);    // Makde white side

            m_cells = new Cells();                  // Initialize the chess cells collection
        }

        // Initialize the chess board and place piece on thier initial positions
        public void Init()
        {
            m_cells.Clear();        // Remove any existing chess cells

            // Build the 64 chess board cells
            for (int row = 1; row <= 8; row++)
                for (int col = 1; col <= 8; col++)
                {
                    m_cells.Add(new Cell(row, col));    // Initialize and add the new chess cell
                }

            String[] rowOrder = Chess960();

            //black setup
            int bRAL = 0;//rowArrayLocation
            string bLetter = "";

            for (int col = 1; col <= 8; col++)
            {
                if (col == 1) bLetter = "a";
                if (col == 2) bLetter = "b";
                if (col == 3) bLetter = "c";
                if (col == 4) bLetter = "d";
                if (col == 5) bLetter = "e";
                if (col == 6) bLetter = "f";
                if (col == 7) bLetter = "g";
                if (col == 8) bLetter = "h";

                if (rowOrder[bRAL] == "R")
                {
                    m_cells[bLetter + "1"].piece = new Piece(Piece.PieceType.Rook, m_BlackSide);
                }
                else if (rowOrder[bRAL] == "N")
                {
                    m_cells[bLetter + "1"].piece = new Piece(Piece.PieceType.Knight, m_BlackSide);
                }
                else if (rowOrder[bRAL] == "B")
                {
                    m_cells[bLetter + "1"].piece = new Piece(Piece.PieceType.Bishop, m_BlackSide);
                }
                else if (rowOrder[bRAL] == "Q")
                {
                    m_cells[bLetter + "1"].piece = new Piece(Piece.PieceType.Queen, m_BlackSide);
                }
                else if (rowOrder[bRAL] == "K")
                {
                    m_cells[bLetter + "1"].piece = new Piece(Piece.PieceType.King, m_BlackSide);
                }

                bRAL++;
            }

            // Now setup the board for black side
            /*m_cells["a1"].piece = new Piece(Piece.PieceType.Rook, m_BlackSide);
            m_cells["h1"].piece = new Piece(Piece.PieceType.Rook, m_BlackSide);
            m_cells["b1"].piece = new Piece(Piece.PieceType.Knight, m_BlackSide);
            m_cells["g1"].piece = new Piece(Piece.PieceType.Knight, m_BlackSide);
            m_cells["c1"].piece = new Piece(Piece.PieceType.Bishop, m_BlackSide);
            m_cells["f1"].piece = new Piece(Piece.PieceType.Bishop, m_BlackSide);
            m_cells["e1"].piece = new Piece(Piece.PieceType.King, m_BlackSide);
            m_cells["d1"].piece = new Piece(Piece.PieceType.Queen, m_BlackSide);*/
            for (int col = 1; col <= 8; col++)
                m_cells[2, col].piece = new Piece(Piece.PieceType.Pawn, m_BlackSide);

            //white setup
            int wRAL = 0;//rowArrayLocation
            string wLetter = "";

            for (int col = 1; col <= 8; col++)
            {
                if (col == 1) wLetter = "a";
                if (col == 2) wLetter = "b";
                if (col == 3) wLetter = "c";
                if (col == 4) wLetter = "d";
                if (col == 5) wLetter = "e";
                if (col == 6) wLetter = "f";
                if (col == 7) wLetter = "g";
                if (col == 8) wLetter = "h";

                if (rowOrder[wRAL] == "R")
                {
                    m_cells[wLetter + "8"].piece = new Piece(Piece.PieceType.Rook, m_WhiteSide);
                }
                else if (rowOrder[wRAL] == "N")
                {
                    m_cells[wLetter + "8"].piece = new Piece(Piece.PieceType.Knight, m_WhiteSide);
                }
                else if (rowOrder[wRAL] == "B")
                {
                    m_cells[wLetter + "8"].piece = new Piece(Piece.PieceType.Bishop, m_WhiteSide);
                }
                else if (rowOrder[wRAL] == "Q")
                {
                    m_cells[wLetter + "8"].piece = new Piece(Piece.PieceType.Queen, m_WhiteSide);
                }
                else if (rowOrder[wRAL] == "K")
                {
                    m_cells[wLetter + "8"].piece = new Piece(Piece.PieceType.King, m_WhiteSide);
                }

                wRAL++;
            }

            // Now setup the board for white side
            /* m_cells["a8"].piece = new Piece(Piece.PieceType.Rook, m_WhiteSide);
             m_cells["h8"].piece = new Piece(Piece.PieceType.Rook, m_WhiteSide);
             m_cells["b8"].piece = new Piece(Piece.PieceType.Knight, m_WhiteSide);
             m_cells["g8"].piece = new Piece(Piece.PieceType.Knight, m_WhiteSide);
             m_cells["c8"].piece = new Piece(Piece.PieceType.Bishop, m_WhiteSide);
             m_cells["f8"].piece = new Piece(Piece.PieceType.Bishop, m_WhiteSide);
             m_cells["e8"].piece = new Piece(Piece.PieceType.King, m_WhiteSide);
             m_cells["d8"].piece = new Piece(Piece.PieceType.Queen, m_WhiteSide);*/
            for (int col = 1; col <= 8; col++)
                m_cells[7, col].piece = new Piece(Piece.PieceType.Pawn, m_WhiteSide);
        }

        public String[] Chess960()
        {
            String[] array = new string[8];
            String[] kingArray = new string[6];

            int randNum = Randomizer(0, 959);

            int bishopCheck = randNum % 16;

            int kingCheck = randNum - bishopCheck;

            if (kingCheck == 0)
            {
                kingArray[0] = "Q";
                kingArray[1] = "N";
                kingArray[2] = "N";
                kingArray[3] = "R";
                kingArray[4] = "K";
                kingArray[5] = "R";
            }
            else if (kingCheck == 16)
            {
                kingArray[0] = "N";
                kingArray[1] = "Q";
                kingArray[2] = "N";
                kingArray[3] = "R";
                kingArray[4] = "K";
                kingArray[5] = "R";
            }
            else if (kingCheck == 32)
            {
                kingArray[0] = "N";
                kingArray[1] = "N";
                kingArray[2] = "Q";
                kingArray[3] = "R";
                kingArray[4] = "K";
                kingArray[5] = "R";
            }
            else if (kingCheck == 48)
            {
                kingArray[0] = "N";
                kingArray[1] = "N";
                kingArray[2] = "R";
                kingArray[3] = "Q";
                kingArray[4] = "K";
                kingArray[5] = "R";
            }
            else if (kingCheck == 64)
            {
                kingArray[0] = "N";
                kingArray[1] = "N";
                kingArray[2] = "R";
                kingArray[3] = "K";
                kingArray[4] = "Q";
                kingArray[5] = "R";
            }
            else if (kingCheck == 80)
            {
                kingArray[0] = "N";
                kingArray[1] = "N";
                kingArray[2] = "R";
                kingArray[3] = "K";
                kingArray[4] = "R";
                kingArray[5] = "Q";
            }
            else if (kingCheck == 96)
            {
                kingArray[0] = "Q";
                kingArray[1] = "N";
                kingArray[2] = "R";
                kingArray[3] = "N";
                kingArray[4] = "K";
                kingArray[5] = "R";
            }
            else if (kingCheck == 112)
            {
                kingArray[0] = "N";
                kingArray[1] = "Q";
                kingArray[2] = "R";
                kingArray[3] = "N";
                kingArray[4] = "K";
                kingArray[5] = "R";
            }
            else if (kingCheck == 128)
            {
                kingArray[0] = "N";
                kingArray[1] = "Q";
                kingArray[2] = "R";
                kingArray[3] = "N";
                kingArray[4] = "K";
                kingArray[5] = "R";
            }
            else if (kingCheck == 144)
            {
                kingArray[0] = "N";
                kingArray[1] = "R";
                kingArray[2] = "N";
                kingArray[3] = "Q";
                kingArray[4] = "K";
                kingArray[5] = "R";
            }
            else if (kingCheck == 160)
            {
                kingArray[0] = "N";
                kingArray[1] = "R";
                kingArray[2] = "N";
                kingArray[3] = "K";
                kingArray[4] = "Q";
                kingArray[5] = "R";
            }
            else if (kingCheck == 176)
            {
                kingArray[0] = "N";
                kingArray[1] = "R";
                kingArray[2] = "N";
                kingArray[3] = "K";
                kingArray[4] = "R";
                kingArray[5] = "Q";
            }
            else if (kingCheck == 192)
            {
                kingArray[0] = "Q";
                kingArray[1] = "N";
                kingArray[2] = "R";
                kingArray[3] = "K";
                kingArray[4] = "N";
                kingArray[5] = "R";
            }
            else if (kingCheck == 208)
            {
                kingArray[0] = "N";
                kingArray[1] = "Q";
                kingArray[2] = "R";
                kingArray[3] = "K";
                kingArray[4] = "N";
                kingArray[5] = "R";
            }
            else if (kingCheck == 224)
            {
                kingArray[0] = "N";
                kingArray[1] = "R";
                kingArray[2] = "Q";
                kingArray[3] = "K";
                kingArray[4] = "N";
                kingArray[5] = "R";
            }
            else if (kingCheck == 240)
            {
                kingArray[0] = "N";
                kingArray[1] = "R";
                kingArray[2] = "K";
                kingArray[3] = "Q";
                kingArray[4] = "N";
                kingArray[5] = "R";
            }
            else if (kingCheck == 256)
            {
                kingArray[0] = "N";
                kingArray[1] = "R";
                kingArray[2] = "K";
                kingArray[3] = "N";
                kingArray[4] = "Q";
                kingArray[5] = "R";
            }
            else if (kingCheck == 272)
            {
                kingArray[0] = "N";
                kingArray[1] = "R";
                kingArray[2] = "K";
                kingArray[3] = "N";
                kingArray[4] = "R";
                kingArray[5] = "Q";
            }
            else if (kingCheck == 288)
            {
                kingArray[0] = "Q";
                kingArray[1] = "N";
                kingArray[2] = "R";
                kingArray[3] = "K";
                kingArray[4] = "R";
                kingArray[5] = "N";
            }
            else if (kingCheck == 304)
            {
                kingArray[0] = "N";
                kingArray[1] = "Q";
                kingArray[2] = "R";
                kingArray[3] = "K";
                kingArray[4] = "R";
                kingArray[5] = "N";
            }
            else if (kingCheck == 320)
            {
                kingArray[0] = "N";
                kingArray[1] = "R";
                kingArray[2] = "Q";
                kingArray[3] = "K";
                kingArray[4] = "R";
                kingArray[5] = "N";
            }
            else if (kingCheck == 336)
            {
                kingArray[0] = "N";
                kingArray[1] = "R";
                kingArray[2] = "K";
                kingArray[3] = "Q";
                kingArray[4] = "R";
                kingArray[5] = "N";
            }
            else if (kingCheck == 352)
            {
                kingArray[0] = "N";
                kingArray[1] = "R";
                kingArray[2] = "K";
                kingArray[3] = "R";
                kingArray[4] = "Q";
                kingArray[5] = "N";
            }
            else if (kingCheck == 368)
            {
                kingArray[0] = "N";
                kingArray[1] = "R";
                kingArray[2] = "K";
                kingArray[3] = "R";
                kingArray[4] = "N";
                kingArray[5] = "Q";
            }
            else if (kingCheck == 384)
            {
                kingArray[0] = "Q";
                kingArray[1] = "R";
                kingArray[2] = "N";
                kingArray[3] = "N";
                kingArray[4] = "K";
                kingArray[5] = "R";
            }
            else if (kingCheck == 400)
            {
                kingArray[0] = "R";
                kingArray[1] = "Q";
                kingArray[2] = "N";
                kingArray[3] = "N";
                kingArray[4] = "K";
                kingArray[5] = "R";
            }
            else if (kingCheck == 416)
            {
                kingArray[0] = "R";
                kingArray[1] = "N";
                kingArray[2] = "Q";
                kingArray[3] = "N";
                kingArray[4] = "K";
                kingArray[5] = "R";
            }
            else if (kingCheck == 432)
            {
                kingArray[0] = "R";
                kingArray[1] = "N";
                kingArray[2] = "N";
                kingArray[3] = "Q";
                kingArray[4] = "K";
                kingArray[5] = "R";
            }
            else if (kingCheck == 448)
            {
                kingArray[0] = "R";
                kingArray[1] = "N";
                kingArray[2] = "N";
                kingArray[3] = "K";
                kingArray[4] = "Q";
                kingArray[5] = "R";
            }
            else if (kingCheck == 464)
            {
                kingArray[0] = "R";
                kingArray[1] = "N";
                kingArray[2] = "N";
                kingArray[3] = "K";
                kingArray[4] = "R";
                kingArray[5] = "Q";
            }
            else if (kingCheck == 480)
            {
                kingArray[0] = "Q";
                kingArray[1] = "R";
                kingArray[2] = "N";
                kingArray[3] = "K";
                kingArray[4] = "N";
                kingArray[5] = "R";
            }
            else if (kingCheck == 496)
            {
                kingArray[0] = "R";
                kingArray[1] = "Q";
                kingArray[2] = "N";
                kingArray[3] = "K";
                kingArray[4] = "N";
                kingArray[5] = "R";
            }
            else if (kingCheck == 512)
            {
                kingArray[0] = "R";
                kingArray[1] = "N";
                kingArray[2] = "Q";
                kingArray[3] = "K";
                kingArray[4] = "N";
                kingArray[5] = "R";
            }
            else if (kingCheck == 528)
            {
                kingArray[0] = "R";
                kingArray[1] = "N";
                kingArray[2] = "K";
                kingArray[3] = "Q";
                kingArray[4] = "N";
                kingArray[5] = "R";
            }
            else if (kingCheck == 544)
            {
                kingArray[0] = "R";
                kingArray[1] = "N";
                kingArray[2] = "K";
                kingArray[3] = "N";
                kingArray[4] = "Q";
                kingArray[5] = "R";
            }
            else if (kingCheck == 560)
            {
                kingArray[0] = "R";
                kingArray[1] = "N";
                kingArray[2] = "K";
                kingArray[3] = "N";
                kingArray[4] = "R";
                kingArray[5] = "Q";
            }
            else if (kingCheck == 576)
            {
                kingArray[0] = "Q";
                kingArray[1] = "R";
                kingArray[2] = "N";
                kingArray[3] = "K";
                kingArray[4] = "R";
                kingArray[5] = "N";
            }
            else if (kingCheck == 592)
            {
                kingArray[0] = "R";
                kingArray[1] = "Q";
                kingArray[2] = "N";
                kingArray[3] = "K";
                kingArray[4] = "R";
                kingArray[5] = "N";
            }
            else if (kingCheck == 608)
            {
                kingArray[0] = "R";
                kingArray[1] = "N";
                kingArray[2] = "Q";
                kingArray[3] = "K";
                kingArray[4] = "R";
                kingArray[5] = "N";
            }
            else if (kingCheck == 624)
            {
                kingArray[0] = "R";
                kingArray[1] = "N";
                kingArray[2] = "K";
                kingArray[3] = "Q";
                kingArray[4] = "R";
                kingArray[5] = "N";
            }
            else if (kingCheck == 640)
            {
                kingArray[0] = "R";
                kingArray[1] = "N";
                kingArray[2] = "K";
                kingArray[3] = "R";
                kingArray[4] = "Q";
                kingArray[5] = "N";
            }
            else if (kingCheck == 656)
            {
                kingArray[0] = "R";
                kingArray[1] = "N";
                kingArray[2] = "K";
                kingArray[3] = "R";
                kingArray[4] = "N";
                kingArray[5] = "Q";
            }
            else if (kingCheck == 672)
            {
                kingArray[0] = "Q";
                kingArray[1] = "R";
                kingArray[2] = "K";
                kingArray[3] = "N";
                kingArray[4] = "N";
                kingArray[5] = "R";
            }
            else if (kingCheck == 688)
            {
                kingArray[0] = "R";
                kingArray[1] = "Q";
                kingArray[2] = "K";
                kingArray[3] = "N";
                kingArray[4] = "N";
                kingArray[5] = "R";
            }
            else if (kingCheck == 704)
            {
                kingArray[0] = "R";
                kingArray[1] = "K";
                kingArray[2] = "Q";
                kingArray[3] = "N";
                kingArray[4] = "N";
                kingArray[5] = "R";
            }
            else if (kingCheck == 720)
            {
                kingArray[0] = "R";
                kingArray[1] = "K";
                kingArray[2] = "N";
                kingArray[3] = "Q";
                kingArray[4] = "N";
                kingArray[5] = "R";
            }
            else if (kingCheck == 736)
            {
                kingArray[0] = "R";
                kingArray[1] = "K";
                kingArray[2] = "N";
                kingArray[3] = "N";
                kingArray[4] = "Q";
                kingArray[5] = "R";
            }
            else if (kingCheck == 752)
            {
                kingArray[0] = "R";
                kingArray[1] = "K";
                kingArray[2] = "N";
                kingArray[3] = "N";
                kingArray[4] = "R";
                kingArray[5] = "Q";
            }
            else if (kingCheck == 768)
            {
                kingArray[0] = "Q";
                kingArray[1] = "R";
                kingArray[2] = "K";
                kingArray[3] = "N";
                kingArray[4] = "R";
                kingArray[5] = "N";
            }
            else if (kingCheck == 784)
            {
                kingArray[0] = "R";
                kingArray[1] = "Q";
                kingArray[2] = "K";
                kingArray[3] = "N";
                kingArray[4] = "R";
                kingArray[5] = "N";
            }
            else if (kingCheck == 800)
            {
                kingArray[0] = "R";
                kingArray[1] = "K";
                kingArray[2] = "Q";
                kingArray[3] = "N";
                kingArray[4] = "R";
                kingArray[5] = "N";
            }
            else if (kingCheck == 816)
            {
                kingArray[0] = "R";
                kingArray[1] = "K";
                kingArray[2] = "N";
                kingArray[3] = "Q";
                kingArray[4] = "R";
                kingArray[5] = "N";
            }
            else if (kingCheck == 832)
            {
                kingArray[0] = "R";
                kingArray[1] = "K";
                kingArray[2] = "N";
                kingArray[3] = "R";
                kingArray[4] = "Q";
                kingArray[5] = "N";
            }
            else if (kingCheck == 848)
            {
                kingArray[0] = "R";
                kingArray[1] = "K";
                kingArray[2] = "N";
                kingArray[3] = "R";
                kingArray[4] = "N";
                kingArray[5] = "Q";
            }
            else if (kingCheck == 864)
            {
                kingArray[0] = "Q";
                kingArray[1] = "R";
                kingArray[2] = "K";
                kingArray[3] = "R";
                kingArray[4] = "N";
                kingArray[5] = "N";
            }
            else if (kingCheck == 880)
            {
                kingArray[0] = "R";
                kingArray[1] = "Q";
                kingArray[2] = "K";
                kingArray[3] = "R";
                kingArray[4] = "N";
                kingArray[5] = "N";
            }
            else if (kingCheck == 896)
            {
                kingArray[0] = "R";
                kingArray[1] = "K";
                kingArray[2] = "Q";
                kingArray[3] = "R";
                kingArray[4] = "N";
                kingArray[5] = "N";
            }
            else if (kingCheck == 912)
            {
                kingArray[0] = "R";
                kingArray[1] = "K";
                kingArray[2] = "R";
                kingArray[3] = "Q";
                kingArray[4] = "N";
                kingArray[5] = "N";
            }
            else if (kingCheck == 928)
            {
                kingArray[0] = "R";
                kingArray[1] = "K";
                kingArray[2] = "R";
                kingArray[3] = "N";
                kingArray[4] = "Q";
                kingArray[5] = "N";
            }
            else if (kingCheck == 944)
            {
                kingArray[0] = "R";
                kingArray[1] = "K";
                kingArray[2] = "R";
                kingArray[3] = "N";
                kingArray[4] = "N";
                kingArray[5] = "Q";
            }

            switch (bishopCheck)
            {
                case 0:
                    {
                        array[0] = "B";
                        array[1] = "B";
                        array[2] = kingArray[0];
                        array[3] = kingArray[1];
                        array[4] = kingArray[2];
                        array[5] = kingArray[3];
                        array[6] = kingArray[4];
                        array[7] = kingArray[5];
                    }
                    break;
                case 1:
                    {
                        array[0] = "B";
                        array[1] = kingArray[0];
                        array[2] = kingArray[1];
                        array[3] = "B";
                        array[4] = kingArray[2];
                        array[5] = kingArray[3];
                        array[6] = kingArray[4];
                        array[7] = kingArray[5];
                    }
                    break;
                case 2:
                    {
                        array[0] = "B";
                        array[1] = kingArray[0];
                        array[2] = kingArray[1];
                        array[3] = kingArray[2];
                        array[4] = kingArray[3];
                        array[5] = "B";
                        array[6] = kingArray[4];
                        array[7] = kingArray[5];
                    }
                    break;
                case 3:
                    {
                        array[0] = "B";
                        array[1] = kingArray[0];
                        array[2] = kingArray[1];
                        array[3] = kingArray[2];
                        array[4] = kingArray[3];
                        array[5] = kingArray[4];
                        array[6] = kingArray[5];
                        array[7] = "B";
                    }
                    break;
                case 4:
                    {
                        array[0] = kingArray[0];
                        array[1] = "B";
                        array[2] = "B";
                        array[3] = kingArray[1];
                        array[4] = kingArray[2];
                        array[5] = kingArray[3];
                        array[6] = kingArray[4];
                        array[7] = kingArray[5];
                    }
                    break;
                case 5:
                    {
                        array[0] = kingArray[0];
                        array[1] = kingArray[1];
                        array[2] = "B";
                        array[3] = "B";
                        array[4] = kingArray[2];
                        array[5] = kingArray[3];
                        array[6] = kingArray[4];
                        array[7] = kingArray[5];
                    }
                    break;
                case 6:
                    {
                        array[0] = kingArray[0];
                        array[1] = kingArray[1];
                        array[2] = "B";
                        array[3] = kingArray[2];
                        array[4] = kingArray[3];
                        array[5] = "B";
                        array[6] = kingArray[4];
                        array[7] = kingArray[5];
                    }
                    break;
                case 7:
                    {
                        array[0] = kingArray[0];
                        array[1] = kingArray[1];
                        array[2] = "B";
                        array[3] = kingArray[2];
                        array[4] = kingArray[3];
                        array[5] = kingArray[4];
                        array[6] = kingArray[5];
                        array[7] = "B";
                    }
                    break;
                case 8:
                    {
                        array[0] = kingArray[0];
                        array[1] = "B";
                        array[2] = kingArray[1];
                        array[3] = kingArray[2];
                        array[4] = "B";
                        array[5] = kingArray[3];
                        array[6] = kingArray[4];
                        array[7] = kingArray[5];
                    }
                    break;
                case 9:
                    {
                        array[0] = kingArray[0];
                        array[1] = kingArray[1];
                        array[2] = kingArray[2];
                        array[3] = "B";
                        array[4] = "B";
                        array[5] = kingArray[3];
                        array[6] = kingArray[4];
                        array[7] = kingArray[5];
                    }
                    break;
                case 10:
                    {
                        array[0] = kingArray[0];
                        array[1] = kingArray[1];
                        array[2] = kingArray[2];
                        array[3] = kingArray[3];
                        array[4] = "B";
                        array[5] = "B";
                        array[6] = kingArray[4];
                        array[7] = kingArray[5];
                    }
                    break;
                case 11:
                    {
                        array[0] = kingArray[0];
                        array[1] = kingArray[1];
                        array[2] = kingArray[2];
                        array[3] = kingArray[3];
                        array[4] = "B";
                        array[5] = kingArray[4];
                        array[6] = kingArray[5];
                        array[7] = "B";
                    }
                    break;
                case 12:
                    {
                        array[0] = kingArray[0];
                        array[1] = "B";
                        array[2] = kingArray[1];
                        array[3] = kingArray[2];
                        array[4] = kingArray[3];
                        array[5] = kingArray[4];
                        array[6] = "B";
                        array[7] = kingArray[5];
                    }
                    break;
                case 13:
                    {
                        array[0] = kingArray[0];
                        array[1] = kingArray[1];
                        array[2] = kingArray[2];
                        array[3] = "B";
                        array[4] = kingArray[3];
                        array[5] = kingArray[4];
                        array[6] = "B";
                        array[7] = kingArray[5];
                    }
                    break;
                case 14:
                    {
                        array[0] = kingArray[0];
                        array[1] = kingArray[1];
                        array[2] = kingArray[2];
                        array[3] = kingArray[3];
                        array[4] = kingArray[4];
                        array[5] = "B";
                        array[6] = "B";
                        array[7] = kingArray[5];
                    }
                    break;
                case 15:
                    {
                        array[0] = kingArray[0];
                        array[1] = kingArray[1];
                        array[2] = kingArray[2];
                        array[3] = kingArray[3];
                        array[4] = kingArray[4];
                        array[5] = kingArray[5];
                        array[6] = "B";
                        array[7] = "B";
                    }
                    break;
            }

            return array;
        }

        public int Randomizer(int min, int max)
        {
            Random random = new Random();
            int randNum = random.Next(min, max);
            return randNum;
        }

        // get the new item by rew and column
        public Cell this[int row, int col]
        {
            get
            {
                return m_cells[row, col];
            }
        }

        // get the new item by string location
        public Cell this[string strloc]
        {
            get
            {
                return m_cells[strloc];
            }
        }

        // get the chess cell by given cell
        public Cell this[Cell cellobj]
        {
            get
            {
                return m_cells[cellobj.ToString()];
            }
        }

        /// <summary>
        /// Serialize the Game object as XML String
        /// </summary>
        /// <returns>XML containing the Game object state XML</returns>
        public XmlNode XmlSerialize(XmlDocument xmlDoc)
        {
            XmlElement xmlBoard = xmlDoc.CreateElement("Board");

            // Append game state attributes
            xmlBoard.AppendChild(m_WhiteSide.XmlSerialize(xmlDoc));
            xmlBoard.AppendChild(m_BlackSide.XmlSerialize(xmlDoc));

            xmlBoard.AppendChild(m_cells.XmlSerialize(xmlDoc));

            // Return this as String
            return xmlBoard;
        }

        /// <summary>
        /// DeSerialize the Board object from XML String
        /// </summary>
        /// <returns>XML containing the Board object state XML</returns>
        public void XmlDeserialize(XmlNode xmlBoard)
        {
            // Deserialize the Sides XML
            XmlNode side = XMLHelper.GetFirstNodeByName(xmlBoard, "Side");

            // Deserialize the XML nodes
            m_WhiteSide.XmlDeserialize(side);
            m_BlackSide.XmlDeserialize(side.NextSibling);

            // Deserialize the Cells
            XmlNode xmlCells = XMLHelper.GetFirstNodeByName(xmlBoard, "Cells");
            m_cells.XmlDeserialize(xmlCells);
        }

        // get all the cell locations on the chess board
        public ArrayList GetAllCells()
        {
            ArrayList CellNames = new ArrayList();

            // Loop all the squars and store them in Array List
            for (int row = 1; row <= 8; row++)
                for (int col = 1; col <= 8; col++)
                {
                    CellNames.Add(this[row, col].ToString()); // append the cell name to list
                }

            return CellNames;
        }

        // get all the cell containg pieces of given side
        public ArrayList GetSideCell(Side.SideType PlayerSide)
        {
            ArrayList CellNames = new ArrayList();

            // Loop all the squars and store them in Array List
            for (int row = 1; row <= 8; row++)
                for (int col = 1; col <= 8; col++)
                {
                    // check and add the current type cell
                    if (this[row, col].piece != null && !this[row, col].IsEmpty() && this[row, col].piece.Side.type == PlayerSide)
                        CellNames.Add(this[row, col].ToString()); // append the cell name to list
                }

            return CellNames;
        }

        // Returns the cell on the top of the given cell
        public Cell TopCell(Cell cell)
        {
            return this[cell.row - 1, cell.col];
        }

        // Returns the cell on the left of the given cell
        public Cell LeftCell(Cell cell)
        {
            return this[cell.row, cell.col - 1];
        }

        // Returns the cell on the right of the given cell
        public Cell RightCell(Cell cell)
        {
            return this[cell.row, cell.col + 1];
        }

        // Returns the cell on the bottom of the given cell
        public Cell BottomCell(Cell cell)
        {
            return this[cell.row + 1, cell.col];
        }

        // Returns the cell on the top-left of the current cell
        public Cell TopLeftCell(Cell cell)
        {
            return this[cell.row - 1, cell.col - 1];
        }

        // Returns the cell on the top-right of the current cell
        public Cell TopRightCell(Cell cell)
        {
            return this[cell.row - 1, cell.col + 1];
        }

        // Returns the cell on the bottom-left of the current cell
        public Cell BottomLeftCell(Cell cell)
        {
            return this[cell.row + 1, cell.col - 1];
        }

        // Returns the cell on the bottom-right of the current cell
        public Cell BottomRightCell(Cell cell)
        {
            return this[cell.row + 1, cell.col + 1];
        }
    }
}
