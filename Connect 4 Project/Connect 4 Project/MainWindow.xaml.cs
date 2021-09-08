using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Connect_4_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {

        public static int[,] ResetBoard()
        {
            int[,] C4Board = new int[6, 7]
            {
                {0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0},
            };
            return C4Board;
        }



        List<List<Label>> labelMap = new List<List<Label>>();

        List<Label> Row0 = new List<Label>();
        List<Label> Row1 = new List<Label>();
        List<Label> Row2 = new List<Label>();
        List<Label> Row3 = new List<Label>();
        List<Label> Row4 = new List<Label>();
        List<Label> Row5 = new List<Label>();

        public MainWindow()
        {
            InitializeComponent();
            Row0.AddRange(new List<Label>() { L0_0, L0_1, L0_2, L0_3, L0_4, L0_5, L0_6 });
            Row1.AddRange(new List<Label>() { L1_0, L1_1, L1_2, L1_3, L1_4, L1_5, L1_6 });
            Row2.AddRange(new List<Label>() { L2_0, L2_1, L2_2, L2_3, L2_4, L2_5, L2_6 });
            Row3.AddRange(new List<Label>() { L3_0, L3_1, L3_2, L3_3, L3_4, L3_5, L3_6 });
            Row4.AddRange(new List<Label>() { L4_0, L4_1, L4_2, L4_3, L4_4, L4_5, L4_6 });
            Row5.AddRange(new List<Label>() { L5_0, L5_1, L5_2, L5_3, L5_4, L5_5, L5_6 });
            labelMap.AddRange(new List<List<Label>>() {Row0, Row1, Row2, Row3, Row4, Row5 });
            TurnCheck();

        }

        int[,] Board = ResetBoard();
        bool P1_Turn = true;
        

        public void TurnCheck()
        {
            if (P1_Turn == true)
            {

                LineUpdates.Content = "Turn: Player 1";
                LineUpdates.Background = Brushes.Yellow;
            }
            else
            {
                LineUpdates.Content = "Turn: Player 2";
                LineUpdates.Background = Brushes.Red;
            }
        }


        public void ColumnCheck(int column)
        {
            for (int row = 5; row >= 0; row--)
            {
                if (Board[row, column] == 0 && P1_Turn == true)
                {
                    Board[row,column] = 1;
                    P1_Turn = !P1_Turn;
                    break;
                }
                else if (Board[row, column] == 0 && P1_Turn == false)
                {
                    Board[row, column] = 2;
                    P1_Turn = !P1_Turn;
                    break;
                }



            }
        }
        private void DisableEButton(Button x, int y)
        {
            if (Board[0,y] == 1 || Board[0,y] == 2)
            {
                x.IsEnabled = false;
            }
        }


        //I need a function that refreshes the labels to match the board.
        public void UpdateBoard()
        {
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    if (Board[row, col] == 0)
                    {
                        // labelMap[row][col].Content = "0";
                        labelMap[row][col].Background = Brushes.White;
                       


                    }
                    else if (Board[row, col] == 1)
                    {
                        //labelMap[row][col].Content = "1"; 
                        labelMap[row][col].Background = Brushes.Yellow;

                    }
                    else if (Board[row,col] == 2)
                    {
                        //labelMap[row][col].Content = "2";
                        labelMap[row][col].Background = Brushes.Red;

                    }
                }
            }
        }



        public string WinCheck(int[,] board)
        {//First is checking horizontal
            for (int i = 0; i < 7; i++)//col
            {
                for(int z =0; z < 3; z++)//rows
                {
                    if(Board[z,i] != 0 && board[z,i] == board[z+1,i] && Board[z+1,i] == board[z+2,i] && board[z+2,i]==board[z+3,i])
                    {
                        if (board[z, i] == 1)
                        {
                            return "Player";
                        }
                        else if (board[z, i] == 2)
                        {
                            return "Ai";
                        }
                    }
                }
            }
            for(int i = 0; i < 6; i++)//rows
            {
                for(int z = 0; z < 4; z++)//Col
                {
                    if(Board[i,z] != 0 && board[i,z] == board[i,z+1] && board[i,z+1] == board[i, z+2] && board[i,z+2] == board[i,z+ 3])
                    {
                       if (board[i, z] == 1)
                        {
                            return "Player";
                        }
                        else if (board[i,z] == 2)
                        {
                            return "Ai";
                        }


                    }
                }
            }
            for (int i = 0; i <= 3; i++)//cols
            {
                for (int z = 0; z < 3; z++)//rows. This is going to positive direction diagonal
                {
                    if (board[z, i] != 0 && board[z, i] == board[z + 1, i + 1] && board[z + 1, i + 1] == board[z + 2, i + 2] && board[z + 2, i + 2] == board[z + 3, i + 3])
                    {
                        if (board[z, i] == 1)
                        {
                            return "Player";
                        }
                        else if (board[z, i] == 2)
                        {
                            return "Ai";
                        }
                    }

                }
            }
            for (int i = 6; i >= 3; i--)//cols
            {
                for (int z = 0; z < 3; z++)//rows. This is going to negative direction diagonal
                {
                    if (board[z, i] != 0 && board[z, i] == board[z + 1, i - 1] && board[z + 1, i - 1] == board[z + 2, i - 2] && board[z + 2, i - 2] == board[z + 3, i - 3])
                    {
                        if (board[z, i] == 1)
                        {
                            return "Player";
                        }
                        else if (board[z, i] == 2)
                        {
                            return "Ai";
                        }
                    }

                }
                List<int> valid_location = new List<int>();
                for(int row = 5; row >= 0; row--)
                {
                    for (int col = 0; col <= 6; col++)
                    {
                        if (Board[row, col] == 0)
                        {
                            valid_location.AddRange(new List<int> { row, col });
                        }
                    }
                }
                if(valid_location.Count == 0)
                {
                    return "Tie";
                }
                    
                
            }
            return "None";
        }

        //public void Ai_move()
        //{
        //  //  int col = minimax(Board, 3, true)[0];
        //    if(col == -1)
        //    {
        //        return;
        //    }
        //    ColumnCheck(col);
        //    UpdateBoard();
        //    //P1_Turn = !P1_Turn;
        //}
        

        private void DisableClick()
        {  
            B0.IsEnabled = false;
            B1.IsEnabled = false;
            B2.IsEnabled = false;
            B3.IsEnabled = false;
            B4.IsEnabled = false;
            B5.IsEnabled = false;
            B6.IsEnabled = false;
        }


        private void B0_Click(object sender, RoutedEventArgs e)
        {
            ColumnCheck(0);
            UpdateBoard();
            //Ai_move();
            TurnCheck();
            DisableEButton(B0, 0);

            string Winner = WinCheck(Board);
            if (Winner == "Player")
            {
                DisableClick();
                LineUpdates.Content = "Player 1 Wins!";
            }
            if (Winner == "Ai")
            {
                DisableClick();
                LineUpdates.Content = "Player 2 Wins!";
            }
            if (Winner == "Tie")
            {
                DisableClick();
                LineUpdates.Content = "Tie";
            }
        }

        private void B1_Click(object sender, RoutedEventArgs e)
        {
            ColumnCheck(1);
            UpdateBoard();
            //Ai_move();
            TurnCheck();
            DisableEButton(B1, 1);
  
            string Winner = WinCheck(Board);
            if (Winner == "Player")
            {
                DisableClick();
                LineUpdates.Content = "Player 1 Wins!";
            }
            if (Winner == "Ai")
            {
                DisableClick();
                LineUpdates.Content = "Player 2 Wins!";
            }
            if (Winner == "Tie")
            {
                DisableClick();
                LineUpdates.Content = "Tie";
            }
        }


        private void B2_Click(object sender, RoutedEventArgs e)
        {
            ColumnCheck(2);
            UpdateBoard();
            //Ai_move();
            TurnCheck();
            DisableEButton(B2, 2);

            string Winner = WinCheck(Board);
            if (Winner == "Player")
            {
                DisableClick();
                LineUpdates.Content = "Player 1 Wins!";
            }
            if (Winner == "Ai")
            {
                DisableClick();
                LineUpdates.Content = "Player 2 Wins";
            }
            if (Winner == "Tie")
            {
                DisableClick();
                LineUpdates.Content = "Tie";
            }
        }


        private void B3_Click(object sender, RoutedEventArgs e)
        {
            ColumnCheck(3);
            UpdateBoard();
            //Ai_move();
            TurnCheck();
            DisableEButton(B3, 3);
     
            string Winner = WinCheck(Board);
            if (Winner == "Player")
            {
                DisableClick();
                LineUpdates.Content = "Player 1 Wins!";
            }
            if (Winner == "Ai")
            {
                DisableClick();
                LineUpdates.Content = "Player 2 Wins";
            }
            if (Winner == "Tie")
            {
                DisableClick();
                LineUpdates.Content = "Tie";
            }
        }


        private void B4_Click(object sender, RoutedEventArgs e)
        {
            ColumnCheck(4);
            UpdateBoard();
            //Ai_move();
            TurnCheck();
            DisableEButton(B4, 4);
   
            string Winner = WinCheck(Board);
            if (Winner == "Player")
            {
                DisableClick();
                LineUpdates.Content = "Player 1 Wins!";
            }
            if (Winner == "Ai")
            {
                DisableClick();
                LineUpdates.Content = "Player 2 Wins";
            }
            if (Winner == "Tie")
            {
                DisableClick();
                LineUpdates.Content = "Tie";
            }
        }


        private void B5_Click(object sender, RoutedEventArgs e)
        {
            ColumnCheck(5);
            UpdateBoard();
            //Ai_move();
            TurnCheck();
            DisableEButton(B5, 5);
   
           string Winner = WinCheck(Board);
            if (Winner == "Player")
            {
                DisableClick();
                LineUpdates.Content = "Player 1 Wins";
            }
            if (Winner == "Ai")
            {
                DisableClick();
                LineUpdates.Content = "Player 2 Wins";
            }
            if (Winner == "Tie")
            {
                DisableClick();
                LineUpdates.Content = "Tie";
            }
        }


        private void B6_Click(object sender, RoutedEventArgs e)
        {
            ColumnCheck(6); 
            UpdateBoard();
            //Ai_move();
            TurnCheck();
            DisableEButton(B6, 6);
  
            string Winner = WinCheck(Board);
            if (Winner == "Player")
            {
                DisableClick();
                LineUpdates.Content = "Player 1 Wins!";
            }
            if (Winner == "Ai")
            {
                DisableClick();
                LineUpdates.Content = "Player 2 Wins";
            }
            if (Winner == "Tie")
            {
                DisableClick();
                LineUpdates.Content = "Tie";
            }
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Board = ResetBoard();
            UpdateBoard();
            B0.IsEnabled = !false;
            B1.IsEnabled = !false;
            B2.IsEnabled = !false;
            B3.IsEnabled = !false;
            B4.IsEnabled = !false;
            B5.IsEnabled = !false;
            B6.IsEnabled = !false;
            P1_Turn = true;
            TurnCheck();
            LineUpdates.Content = "";

        }
        private bool is_terminal_node(int[,] board)
        {
            if (WinCheck(board) == "Player")
            {
                return true;
            }
            else if (WinCheck(board) == "Ai")
            {
                return true;
            }
            else if (WinCheck(board) == "Tie")
            {
                return true;
            }
            else return false; 
            
        }

        //public int Ai_score(int[,] board, int player)
        //{
        //    int x = player;
        //    int score = 0;
        //    for (int i = 0; i < 7; i++)//col
        //    {
        //        for (int z = 0; z < 3; z++)//rows
        //        {
        //            if (board[z, i] != 0 && board[z, i] == Board[z + 1, i] && board[z + 1, i] == board[z + 2, i] && Board[z + 2, i] == board[z + 3, i] && board[z,i] == x)
        //            {
        //                score += 100;
        //            }
        //            else if (board[z, i] != 0 && board[z, i] == board[z + 1, i] && board[z + 1, i] == board[z + 2, i] && board[z,i] == x)
        //            {
        //                score +=  10;
        //            }


        //        }
        //    }
        //    for (int i = 0; i < 6; i++)//rows
        //    {
        //        for (int z = 0; z < 4; z++)//Col
        //        {
        //            if (board[i,z]!= 0 && board[i, z] == board[i, z + 1] && board[i, z + 1] == board[i, z + 2] && board[i, z + 2] == board[i, z + 3] && board[z,i] == x)
        //            {
        //                score += 100;

        //            }
        //            else if ( board[i,z] != 0 && board[i, z] == board[i, z + 1] && board[i, z + 1] == board[i, z + 2] && board[i,z] == x)
        //            {
        //                score += 10;
        //            }
        //        }
        //    }
        //    for (int i = 0; i <= 3; i++)//cols
        //    {
        //        for (int z = 0; z < 3; z++)//rows. This is going to positive direction diagonal
        //        {
        //            if (board[z, i] == board[z + 1, i + 1] && board[z + 1, i + 1] == board[z + 2, i + 2] && board[z + 2, i + 2] == board[z + 3, i + 3] && board[z,i] == x)
        //            {
        //                score += 100;
        //            }
        //            else if(Board[z,i] != 0 && board[z, i] == board[z + 1, i + 1] && board[z + 1, i + 1] == board[z + 2, i + x)
        //            {
        //                score += 10;
        //            }

        //        }
        //    }
        //    for (int i = 6; i >= 3; i--)//cols
        //    {
        //        for (int z = 0; z < 3; z++)//rows. This is going to negative direction diagonal
        //        {
        //            if (board[z,i] != 0 && board[z, i] == board[z + 1, i - 1] && board[z + 1, i - 1] == board[z + 2, i - 2] && board[z + 2, i - 2] == board[z + 3, i - 3] && board[z,i] == x)
        //            {
        //                score += 100;
        //            }
        //            else if (board[z,i] != 0 && board[z, i] == board[z + 1, i - 1] && board[z + 1, i - 1] == board[z + 2, i - 2] && board[z,i] == x)
        //            {
        //                score += 10;
        //            }

        //        }
                


        //    }
        //    return score;
        //}
        //public int[] minimax(int[,] board, int depth, bool maximisingPlayer)
        //{
        //    if (depth == 0 || is_terminal_node(board) == true)
        //    {
        //        if (is_terminal_node(board) == true)
        //        {
        //            if (WinCheck(board) == "Ai")
        //            {
        //                int[] tempArray = new int[] { -1, 10000000 };
        //                return tempArray;
        //            }
        //            else if (WinCheck(board) == "Player")
        //            {
        //                int[] tempArray = new int[] { -1, -1000000 };
        //                return tempArray;
        //            }
        //            else
        //            {
        //                int[] tempArray = new int[] { -1, 0 };
        //                return tempArray;
        //            }

        //        }
        //    else
        //    {
        //            int[] tempArray = new int[] {-1,Ai_score(board, 2)};
        //            return tempArray;
        //    }
        //    }
        //    if (maximisingPlayer == true)
        //    {
        //        int value = int.MinValue;
        //        int[] tempArray = new int[] { -1, value };
        //        for (int col = 0; col < 7; col++)
        //        {
        //            for (int row = 5; row >= 0; row--)
        //            {
        //                int[,] tempBoard = Board.Clone() as int[,];
        //                if (tempBoard[row, col] == 0)
        //                {
        //                    tempBoard[row, col] = 2;
        //                    int tempScore = minimax(tempBoard, depth - 1, false)[1];
        //                    if (tempScore > value)
        //                    {
        //                        value = tempScore;
        //                        tempArray[0] = col;
        //                        tempArray[1] = value;
        //                        break;
        //                    }
                            
        //                }
                       
        //            }
                   
                    
        //        }
        //          return tempArray;
                

        //    }
            
        //    else
        //    {
        //        int value = int.MaxValue;
        //        int[] tempArray = new int[] { -1, value };
        //        for (int col = 0; col < 7; col++)
        //        {
        //            for (int row = 5; row >= 0; row--)
        //            {
        //                int[,] tempBoard = Board.Clone() as int[,];
        //                if (tempBoard[row, col] == 0)
        //                {
                            
        //                    tempBoard[row, col] = 1;
        //                    int tempScore = minimax(tempBoard, depth - 1, true)[1];
        //                    if (tempScore < value)
        //                    {
        //                        value = tempScore;
        //                        tempArray[0] = col;
        //                        tempArray[1] = value;
        //                        break;
        //                    }
                            
                            

        //                }
                       
        //            }
                   
        //        }
        //         return tempArray;
        //    }
            

        }




    }


    
