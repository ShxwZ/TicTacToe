using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Cuadriculas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int turno = 0;
        private int[,] tablero;
        private Boolean fin;
        private List<Button> buttons;
        private int nJugadas;
        public MainWindow()
        {
            InitializeComponent();
            tablero = new int[3,3];
            buttons = new List<Button>
            {
                boton00,boton01,boton02,
                boton10,boton11,boton12,
                boton20,boton21,boton22
            };

            initGame();

        }

        private void initGame() {
            fin = false;
            fillArray(tablero);
            nJugadas = 0;
            setTurnLabel();
        }

        private void reiniciarJuego(object sender, RoutedEventArgs e) {
            turnoLabel.Visibility = Visibility.Visible;
            buttons.ForEach(b =>
            {
                changeImgButton(b, new Uri("dibujos/blank.png", UriKind.RelativeOrAbsolute));
                b.IsEnabled= true;
                ganadorLabel.Content = "";

            });
            initGame();
        }

        private void setTurnLabel() {
            turnoLabel.Content = "Turno de " + (turno == 0 ? "✖" : "⭕");
        }

        private void fillArray(int[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = -1;
                }
            }
        }

        private void playTurn(object sender, RoutedEventArgs e) {
            nJugadas++;
            Button button= (Button)sender;
            setImgButton(button);
            tablero[Grid.GetColumn(button), Grid.GetRow(button)] = turno;
            checkWinner();
            turno = turno == 0 ? 1 : 0;
            setTurnLabel();
            button.IsEnabled = false;
        }

        private void changeImgButton(Button button, Uri img) {
            ControlTemplate ct = button.Template;
            Image imagenDelBoton = (Image)ct.FindName("imgLineItemAdd", button);
            imagenDelBoton.Source = new BitmapImage(img);
            
        }

        private void setImgButton(Button button) {
            int nDibujo = new Random().Next(1, 3);

            if (turno == 0)
            {
                changeImgButton(button, new Uri("dibujos/x" + nDibujo + ".png", UriKind.RelativeOrAbsolute));
            } else if (turno == 1)
            {
                changeImgButton(button, new Uri("dibujos/circulo" + nDibujo + ".png", UriKind.RelativeOrAbsolute));
            }
        }

        private void checkWinner() {
            if (checkColumns() || checkRows() || checkDiagonal())
            {
                String ganador = turno == 0 ? "✖" : "⭕";
                ganadorLabel.Content = "Gana " + ganador + " 🥳🥳";
                buttons.ForEach((b) => b.IsEnabled= false);
                turnoLabel.Visibility = Visibility.Hidden;
            }
            else if (nJugadas == 9){
                ganadorLabel.Content = "Empate";
                turnoLabel.Visibility = Visibility.Hidden;
            }
        
        }

        private Boolean checkColumns() {
            for (int i = 0; i < 3; i++)
            {
                int uno = 0;
                int cero = 0;
                for (int j = 0; j < 3; j++)
                {
                    if (tablero[j, i] == 1)
                        uno++;
                    else if (tablero[j, i] == 0)
                        cero++;
                }
                if (uno == 3 || cero == 3)
                    return true;
            }
            return false;
        }
        private Boolean checkRows()
        {
            for (int i = 0; i < 3; i++)
            {
                int uno = 0;
                int cero = 0;
                for (int j = 0; j < 3; j++)
                {
                    if (tablero[i, j] == 1)
                        uno++;
                    else if (tablero[i, j] == 0)
                        cero++;
                }
                if (uno == 3 || cero == 3)
                    return true;
            }
            return false;
        }

        private Boolean checkDiagonal() {
            int uno = 0;
            int cero = 0;

            for (int i = 0; i < 3; i++)
            {
                if (tablero[i, i] == 1)
                    uno++;
                else if (tablero[i, i] == 0)
                    cero++;
            }

            if (uno == 3 || cero == 3)
                return true;

            uno = 0;
            cero = 0;

            for (int i = 0; i < 3; i++)
            {
                if (tablero[i, 2 - i] == 1)
                    uno++;
                else if (tablero[i, 2 - i] == 0)
                    cero++;
            }

            if (uno == 3 || cero == 3)
                return true;

            return false;
        }



    }

}
