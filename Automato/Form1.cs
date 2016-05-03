using SdlDotNet.Core;
using SdlDotNet.Graphics;
using SdlDotNet.Graphics.Primitives;
using SdlDotNet.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Automato
{
    public partial class Form1 : Form
    {

        //A surface é a representação da tela
        //A tela principal é a Screen, e agrega todas as outras
        private Surface Screen;
        private Surface SurfaceTextCoordinate;
        private Surface SurfaceTextStatus;
        private Surface SurfaceTextMenu;

        //Esquema de cores
        Color backgroundColor = Color.White;
        Color foregroundColor = Color.Black;

        //Exemplo de uma instância de cor RGBA
        Color customColor = Color.FromArgb(8, 255, 0, 0);
        
        //Variáveis de estado do programa
        private static int x = 0; //Coordenada X
        private static int y = 0; //Coordenada Y
        private char PreviousCommand = ' '; //Comando Anterior
        private static char Command = ' '; //Proximo Comando

        //Legendas
        private string Status = "Ready"; //Texto do status
        private string  Menu = "M: Mover | E: Inserir novo estado | Delete: Deletar Elementos | T: Adicionar Transição | I: Definir estado inicial"; //Texto do Menu
        private string Debug = "Debuging...";


        //Representação global dos automatos
        private List<char> Alphabet; //Alfabeto
        Triangle initialState = new Triangle(new Point(0, 0), new Point(10, 10), new Point(0, 20)); //Seta do Estado Inicial
        List<Node> listNodes = new List<Node>(); //Lista com os Nós (Ou Estados)
        List<Transition> listTransition = new List<Transition>(); //Lista com as transições
        
        //Nodes temporários (usados no comando de transição)
        Node from = null, to = null, selected = null;
        private Surface SurfaceTextDebug;
        private Line linhaTransicao;
        private Triangle setaTransicao;

        public Form1()
        {
            InitializeComponent();
        }


        public void IniciarSdl()
        {
            Screen = Video.SetVideoMode(1024, 500, false, false, false);
            Video.WindowCaption = "Simulador de Automatos";
            SdlDotNet.Graphics.Font font = new SdlDotNet.Graphics.Font(@"C:\Windows\Fonts\Arial.ttf", 12);

            SdlDotNet.Core.Events.Fps = 120;

            SdlDotNet.Core.Events.Tick += new EventHandler<TickEventArgs>(delegate (object sender, TickEventArgs args)
            {


                SdlDotNet.Core.Events.MouseButtonDown += new EventHandler<MouseButtonEventArgs>(MouseButtonDown);
                SdlDotNet.Core.Events.MouseMotion += new EventHandler<MouseMotionEventArgs>(MouseMotionEvent);
                SdlDotNet.Core.Events.KeyboardUp += new EventHandler<KeyboardEventArgs>(KeyboardPress);
                SdlDotNet.Core.Events.MouseButtonUp += new EventHandler<MouseButtonEventArgs>(MouseButtonUp);


                string Position = string.Format("Posição X: {0} Y: {1}", x, y);
                Status = GetStatus(Command);

                Screen.Fill(backgroundColor);

                //Exibir as linhas de transição
                foreach (var item in this.listTransition)
                {
                    //Renderização da linha
                    Line p = new Line(item.From.Coordenada, item.To.Coordenada);

                    //Calcular a posição do Element da Transition
                    Point pontoMedio = Calculos.GetPontoMedio(p.Point1, p.Point2);

                    ArrayList pontosBezier = new ArrayList();
                    pontosBezier.Add(item.To.Coordenada);
                    pontosBezier.Add(new Point(pontoMedio.X, pontoMedio.Y + 100));
                    pontosBezier.Add(item.From.Coordenada);

                    Bezier curvaBezier = new Bezier(pontosBezier, 3);
                    curvaBezier.Center = new Point(pontoMedio.X, pontoMedio.Y);


                    p.Draw(Screen, foregroundColor, true);

                    
                    //Imprime o elemento de transição
                    string element = item.Element.ToString();
                    Surface transitionElementText = font.Render(element, foregroundColor);
                    Screen.Blit(transitionElementText, pontoMedio);

                    //Desenha """"""""a seta"""""""""""
                    //Inicialmente, pegamos a posição do centro do circulo destino
                    Point centroCircunferencia = item.To.Coordenada;

                    //Pegamos a posição da seta
                    Point setaPosition = Calculos.GetSetaPosition(p, centroCircunferencia);
                    int angulo = Calculos.GetAnguloReta(p.Point1, p.Point2);

                    Triangle seta = Calculos.GetArrow(setaPosition, angulo);
                    seta.Draw(Screen, foregroundColor, true, true);


                }



                if (this.linhaTransicao != null)
                {
                    this.linhaTransicao.Draw(Screen, Color.Red);
                    this.setaTransicao.Draw(Screen, Color.Red, true, true);
                }

                //Exibir automatos
                foreach (var item in this.listNodes)
                {
                    Ellipse node = new Ellipse(item.Coordenada, new Size(20, 20));

                    if (item.Estado == Estado.InicialAceitacao)
                    {
                        //Desenha o node
                        node.Draw(Screen, Color.Green, true, true);

                        //Desenha o circulo menor interno, 
                        Ellipse circuloMenor = new Ellipse(item.Coordenada, new Size(15, 15));
                        circuloMenor.Draw(Screen, Color.Black);

                        //Desenha a seta indicando o estado inicia
                        initialState.Center = new Point(item.Coordenada.X - 20, item.Coordenada.Y);
                        initialState.Draw(Screen, Color.Red, true, true);



                        Surface nodeName = font.Render(item.Nome, Color.White);
                        Screen.Blit(nodeName, new Point(item.Coordenada.X - 7, item.Coordenada.Y - 7));

                    }
                    else if (item.Estado == Estado.InicialNaoAceitacao)
                    {
                        //Desenha o node
                        node.Draw(Screen, Color.Green, true, true);

                        //Desenha a seta indicando o estado inicia
                        initialState.Center = new Point(item.Coordenada.X - 20, item.Coordenada.Y);
                        initialState.Draw(Screen, Color.Red, true, true);

                        Surface nodeName = font.Render(item.Nome, Color.White);
                        Screen.Blit(nodeName, new Point(item.Coordenada.X - 7, item.Coordenada.Y - 7));
                    }
                    else if (item.Estado == Estado.Aceitacao)
                    {
                        node.Draw(Screen, Color.Green, true, true);
                        Surface nodeName = font.Render(item.Nome, Color.White);
                        Screen.Blit(nodeName, new Point(item.Coordenada.X - 7, item.Coordenada.Y - 7));

                        //Desenha o circulo menor interno, 
                        Ellipse circuloMenor = new Ellipse(item.Coordenada, new Size(15, 15));
                        circuloMenor.Draw(Screen, Color.Black);


                    }
                    else if (item.Estado == Estado.NaoAceitacao)
                    {
                        node.Draw(Screen, Color.Green, true, true);
                        Surface nodeName = font.Render(item.Nome, Color.White);
                        Screen.Blit(nodeName, new Point(item.Coordenada.X - 7, item.Coordenada.Y - 7));
                    }

                }



                SurfaceTextCoordinate = font.Render(Position, foregroundColor);
                SurfaceTextStatus = font.Render(Status, foregroundColor);
                SurfaceTextMenu = font.Render(this.Menu, foregroundColor);
                SurfaceTextDebug = font.Render(this.Debug, foregroundColor);

                Screen.Blit(SurfaceTextCoordinate, new Point(890, 0));
                Screen.Blit(SurfaceTextMenu, new Point(0, 0));
                Screen.Blit(SurfaceTextStatus, new Point(0, 450));
                Screen.Blit(SurfaceTextDebug, new Point(500, 450));

                Debug = "Debugging";

                Screen.Update();
            });

            SdlDotNet.Core.Events.Run();
        }

        private void MouseButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (Command == 'M')
                this.selected = null;
            else if(Command == 'T')
            {
                Node selectedNode = GetClickedNode();
                if(selectedNode != null)
                {
                    char elem;

                    GetElementTransition getElementTransition = new GetElementTransition(this.Alphabet);
                    getElementTransition.ShowDialog();
                    elem = getElementTransition.Element;
                    getElementTransition.Close();

                    this.listTransition.Add(new Transition(from, selectedNode, elem));
                }

                this.linhaTransicao = new Line();
                this.setaTransicao = new Triangle();
                this.from = null;
                this.to = null;
                Command = ' ';
            }

        }

        private void KeyboardPress(object sender, KeyboardEventArgs e)
        {
            if (e.Key == (Key.Delete))
                Command = 'D';
            else if (e.Key == (Key.E))
                Command = 'E';
            else if (e.Key == (Key.T))
                Command = 'T';
            else if (e.Key == (Key.S))
                Command = 'S';
            else if (e.Key == (Key.M))
                Command = 'M';
            else if (e.Key == (Key.K))
                Command = 'K';
            else if (e.Key == (Key.I))
                Command = 'I';
            else if (e.Key == (Key.Q))
                Command = 'Q';
        }

        private void MouseButtonDown(object sender, MouseButtonEventArgs args)
        {
            if (args.Button == MouseButton.PrimaryButton)
            {
                if (Command == 'E')
                {
                    string Nome = GenerateNodeName();
                    this.listNodes.Add(new Node(Nome, new Point(x, y), Estado.NaoAceitacao));
                    this.PreviousCommand = 'E';
                    Command = ' ';
                }
                else if (Command == 'D')
                {
                    Node toDelete = GetClickedNode();
                    if (toDelete != null)
                    {
                        this.listTransition.RemoveAll(p => p.From.Nome == toDelete.Nome || p.To.Nome == toDelete.Nome);
                        listNodes.Remove(toDelete);
                    }
                    this.PreviousCommand = 'D';
                    Command = ' ';
                }
                else if (Command == 'T' && this.from == null)
                {
                    this.from = GetClickedNode();
                }
                else if (Command == 'S' && this.PreviousCommand != 'T')
                {
                    this.PreviousCommand = 'S';
                    Command = ' ';
                    return;
                }
                else if (Command == 'M')
                {
                    this.selected = GetClickedNode();
                    if (this.selected != null)
                        SdlDotNet.Core.Events.MouseMotion += new EventHandler<MouseMotionEventArgs>(ArrastarNode);
                    else
                        return;

                }
                else if(Command == 'I')
                {
                    Node setInitial = GetClickedNode();
                    Node inicialState = this.listNodes.Find(p => p.Estado == Estado.InicialAceitacao || p.Estado == Estado.InicialNaoAceitacao);
                    if(setInitial != null)
                    {
                        if(inicialState != null)
                        {
                            if (inicialState.Estado == Estado.InicialAceitacao)
                                inicialState.Estado = Estado.Aceitacao;
                            else
                                inicialState.Estado = Estado.NaoAceitacao;
                        }

                        if (setInitial.Estado == Estado.Aceitacao)
                            setInitial.Estado = Estado.InicialAceitacao;
                        else
                            setInitial.Estado = Estado.InicialNaoAceitacao;

                    }

                    Command = ' ';
                    this.PreviousCommand = 'I';
                }



            }
            else if(args.Button == MouseButton.SecondaryButton)
            {
                Node toChange = GetClickedNode();
                if (toChange != null)
                {
                    if (toChange.Estado == Estado.InicialAceitacao)
                        toChange.Estado = Estado.InicialNaoAceitacao;
                    else if (toChange.Estado == Estado.InicialNaoAceitacao)
                        toChange.Estado = Estado.InicialAceitacao;
                    else if (toChange.Estado == Estado.NaoAceitacao)
                        toChange.Estado = Estado.Aceitacao;
                    else if (toChange.Estado == Estado.Aceitacao)
                        toChange.Estado = Estado.NaoAceitacao;
                }
                
            }
        }

        private void ArrastarNode(object sender, MouseMotionEventArgs e)
        {
            if (this.selected != null)
            {
                this.selected.Coordenada.X = x;
                this.selected.Coordenada.Y = y;
            }
        }

        private Node GetClickedNode()
        {

            foreach (var item in this.listNodes)
	        {
                //Tomemos o centro da circunferência:
                Point centroCircunferência = item.Coordenada;

                //Usando a equação geral da circunferência temos:            
                if (Math.Sqrt(Math.Pow((x - centroCircunferência.X), 2) + Math.Pow(y - centroCircunferência.Y, 2)) <= 100)
                    return item;
	        }

            return null;
        }

        private string GenerateNodeName()
        {
            int count = 0;
            for (count = 0; count < listNodes.Count; count++)
            {
                if (this.listNodes.Find(p => p.Nome == string.Format("q{0}", count)) == null)
                    return string.Format("q{0}", count);
            }

            return string.Format("q{0}", count);
        }

        private void MouseMotionEvent(object sender, MouseMotionEventArgs args)
        {
            x = args.Position.X;
            y = args.Position.Y;

            if(from != null && Command == 'T')
            {
                this.linhaTransicao = new Line(this.from.Coordenada, new Point(x, y));
                int angulo = Calculos.GetAnguloReta(this.linhaTransicao.Point1, this.linhaTransicao.Point2);
                this.setaTransicao = Calculos.GetArrow(new Point(x, y), angulo);

                Debug = "Line";
            }

        }

        private void btnDefinir_Click(object sender, EventArgs e)
        {
            this.Alphabet = new List<char>();
            
            foreach (var item in txtAlfabeto.Text.Split(',').ToList())
                this.Alphabet.Add(char.Parse(item));
            
            this.Hide();
            IniciarSdl();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            SdlDotNet.Core.Events.QuitApplication();
        }

        public string GetStatus(char op)
        {
            if (op == 'D')
                return "Clique no elemento para deletar!";
            else if (op == 'E')
                return "Clique para adicionar um novo estado!";
            else if (op == 'M')
                return "Clique no Item, e mova para a nova posição";
            else if (op == 'T')
                return "Digite a letra do alfabeto, clique no estado de origem, e então pressione S";
            else if (op == 'S' && this.PreviousCommand == 'T')
                return "Clique no estado de destino";
            else if (op == 'S' && this.PreviousCommand != 'T')
                return "Para adicionar uma transição, pressione T primeiramente";
            else if (op == 'I')
                return "Clique no estado que deseja tornar inicial";
            else return "Ready";
        }

    }
}
