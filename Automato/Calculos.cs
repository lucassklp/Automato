using SdlDotNet.Graphics.Primitives;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automato
{
    class Calculos
    {


        public static Point GetFinalPointRotate(Point origin, Point destination, double Angulo)
        {
            int x;
            int y;
            //Angulo = Angulo * (Math.PI / 180);
            x = Convert.ToInt32(Math.Cos(Angulo) * (destination.X - origin.X) - Math.Sin(Angulo % 90) * ((destination.Y - origin.Y) + origin.X));
            y = Convert.ToInt32(Math.Sin(Angulo) * (destination.X - origin.X) + Math.Cos(Angulo % 90) * ((destination.Y - origin.Y) + origin.Y));

            return new Point(x, y);
        }

        

        public static double GetAnguloReta(Point pt1, Point pt2)
        {
            float dx = pt2.X - pt1.X;
            float dy = pt2.Y - pt1.Y;

            double deg = Math.Atan2(dy, dx) * (180 / Math.PI);
            if (deg < 0)
                deg += 360;

            return deg;
        }

        //public static double GetAnguloReta(Line reta)
        //{
        //    double By = reta.Point2.Y;
        //    double Ay = reta.Point1.Y;
        //    double Bx = reta.Point2.X;
        //    double Ax = reta.Point1.X;
        //    double coef = (By - Ay) / (Bx - Ax);
        //    return Math.Atan2(coef); //angle = atan((By - Ay) / (Bx - Ax))
        //}


        /// <summary>
        /// Função usada para calcular o ponto médio
        /// </summary>
        /// <param name="origem">Ponto de Origem</param>
        /// <param name="destino">Ponto de Destino</param>
        /// <returns>O ponto médio entre o ponto de origem e o ponto de destino</returns>
        public static Point GetPontoMedio(Point origem, Point destino)
        {
            return new Point(Math.Abs((origem.X + destino.X) / 2), Math.Abs((origem.Y + destino.Y) / 2));
        }


        /// <summary>
        /// Função usada para calcular a distancia entre dois pontos
        /// </summary>
        /// <param name="origem">Ponto de origem</param>
        /// <param name="destino">Ponto de destino</param>
        /// <returns>O módulo da distância entre os dois pontos</returns>
        public static int GetDistanciaEntreDoisPontos(Point origem, Point destino)
        {
            return (int)Math.Abs(Math.Sqrt(Math.Pow(destino.X - origem.X, 2) + Math.Pow(destino.Y - origem.Y, 2)));
        }



        /// <summary>
        /// Função usada para informar se um ponto pertence a uma reta
        /// </summary>
        /// <param name="a">Coeficiente 'a' da equacao geral da reta</param>
        /// <param name="b">Coeficiente 'b' da equacao geral da reta</param>
        /// <param name="c">Coeficiente 'c' da equacao geral da reta</param>
        /// <param name="ponto">Ponto que deseja verificar</param>
        /// <returns>Retorna se pertence ou não</returns>
        private static bool VerificarSePontoPertenceAReta(double a, double b, double c, Point ponto)
        {
            int result = Convert.ToInt32(Math.Abs(a * ponto.X + b * ponto.Y + c) / (Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2))));
            return (result == 0);
        }


        /// <summary>
        /// Função usada para pegar a posição da seta
        /// </summary>
        /// <param name="linha">Linha usada para indicar a transição</param>
        /// <param name="centroCircunferenciaDestino">Centro da circunferência destino</param>
        /// <returns>A coordenada da seta</returns>
        public static Point GetSetaPosition(Line linha, Point centroCircunferenciaDestino)
        {
            double a, b, c;
            double y1 = linha.Point1.Y;
            double y2 = linha.Point2.Y;
            double x1 = linha.Point1.X;
            double x2 = linha.Point2.X;
            int Distancia = int.MaxValue;

            //Equação geral da reta =>  ax + by + c = 0
            a = y1 - y2;
            b = x2 - x1;
            c = (x1 - x2) * y1 + (y2 - y1) * x1;

            //Para certificar que a 'seta' irá ficar na borda do, é necessário escolher um ponto que obedeça a segunte expressão: (x-a)² + (x-b)² = r² e d = 0, então:
            List<Point> listPontos = new List<Point>();
            List<Point> pontosPertencentesAReta = new List<Point>();
            Point pontoCorreto = new Point();

            for (int angulo = 0; angulo < 360; angulo++)
                listPontos.Add(PontoNoCirculo(Constantes.Diametro, angulo, centroCircunferenciaDestino));



            //verificando quais da lista pertentem a reta
            foreach (var itemPonto in listPontos)
            {
                if (VerificarSePontoPertenceAReta(a, b, c, itemPonto))
                    pontosPertencentesAReta.Add(itemPonto);
            }

            //Dos pontos pertencentes a reta, pegar o que tem menor distância
            foreach (var item in pontosPertencentesAReta)
            {
                int dist = GetDistanciaEntreDoisPontos(linha.Point1, item);
                if (dist < Distancia)
                {
                    Distancia = dist;
                    pontoCorreto = item;
                }
            }
            return pontoCorreto;
        }




        /// <summary>
        /// O círculo é a linha que delimita a circunferência e o espaço externo.
        /// </summary>
        /// <param name="Raio">Raio da circunferência</param>
        /// <param name="Angulo">Angulo Desejado</param>
        /// <param name="Origem">O ponto de origem da circunferência</param>
        /// <returns>Essa função retorna um ponto, num determinado angulo, pertencente ao circulo</returns>
        private static Point PontoNoCirculo(int Raio, int Angulo, Point centroCircunferencia)
        {
            float x = (float)(Raio * Math.Cos(Angulo * Math.PI / 180F)) + centroCircunferencia.X;
            float y = (float)(Raio * Math.Sin(Angulo * Math.PI / 180F)) + centroCircunferencia.Y;

            return new Point(Convert.ToInt32(x), Convert.ToInt32(y));
        }



    }
}
