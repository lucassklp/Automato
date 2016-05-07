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

        /// <summary>
        /// Verifica se um ponto pertence ao circulo/circunferência
        /// </summary>
        /// <param name="ponto">O ponto que deseja verificar</param>
        /// <param name="centroCircunferência">O centro da circunferencia</param>
        /// <returns>Uma boolean representando se pertence ou não.</returns>
        public static bool PontoPertenceACircunferencia(Point ponto, Point centroCircunferência)
        {
            return (Math.Sqrt(Math.Pow((ponto.X - centroCircunferência.X), 2) + Math.Pow(ponto.Y - centroCircunferência.Y, 2)) <= 20);
        }

        /// <summary>
        /// Função usada para renderizar a ponta da seta
        /// </summary>
        /// <param name="setaPosition">A posição em que a seta apontará</param>
        /// <param name="angulo">O ângulo de inclinação da seta</param>
        /// <returns>Uma seta na determinada posição, na determinada inclinação</returns>
        public static Triangle GetArrow(Point setaPosition, int angulo)
        {
            if (angulo % 46 == 45)
                angulo++;

            Point pontoCentro = setaPosition;
            Point ponto1 = new Point(setaPosition.X - 5, setaPosition.Y - 5);
            Point ponto2 = new Point(setaPosition.X - 5, setaPosition.Y + 5);

            ponto1 = RotatePoint(ponto1, setaPosition, angulo);
            ponto2 = RotatePoint(ponto2, setaPosition, angulo);

            Triangle seta = new Triangle(pontoCentro, ponto1, ponto2);
            return seta;
        }


        /// <summary>
        /// Função usada para rotacionar um ponto em relação a um dado ponto central
        /// </summary>
        /// <param name="pointToRotate">O ponto que irá se movimentar (o ponto que será rotacionado)</param>
        /// <param name="centerPoint">O ponto central</param>
        /// <param name="angleInDegrees">O ângulo de rotação (em graus)</param>
        /// <returns>A posição do ponto após a rotação</returns>
        private static Point RotatePoint(Point pointToRotate, Point centerPoint, double angleInDegrees)
        {
            double angleInRadians = angleInDegrees * (Math.PI / 180);
            double cosTheta = Math.Cos(angleInRadians);
            double sinTheta = Math.Sin(angleInRadians);
            return new Point
            {
                X =
                    (int)
                    (cosTheta * (pointToRotate.X - centerPoint.X) -
                    sinTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.X),
                Y =
                    (int)
                    (sinTheta * (pointToRotate.X - centerPoint.X) +
                    cosTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.Y)
            };
        }


        /// <summary>
        /// Função usada para descobrir o ângulo da reta
        /// </summary>
        /// <param name="pt1">Origem da Reta</param>
        /// <param name="pt2">Destino da Reta</param>
        /// <returns>O ângulo (em graus) formado por essa reta</returns>
        public static int GetAnguloReta(Point pt1, Point pt2)
        {
            float dx = pt2.X - pt1.X;
            float dy = pt2.Y - pt1.Y;

            int deg = Convert.ToInt32(Math.Atan2(dy, dx) * (180 / Math.PI));
            if (deg < 0)
                deg += 360;

            return deg;
        }



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
        /// Função usada para informar se um ponto pertence a uma reta => (ax + by + c) = 0
        /// </summary>
        /// <param name="a">Coeficiente 'a' da equacao geral da reta</param>
        /// <param name="b">Coeficiente 'b' da equacao geral da reta</param>
        /// <param name="c">Coeficiente 'c' da equacao geral da reta</param>
        /// <param name="ponto">Ponto que deseja verificar</param>
        /// <returns>Retorna se pertence ou não</returns>
        private static bool VerificarSePontoPertenceAReta(double a, double b, double c, Point ponto)
        {
            if (a == 0 && b == 0)
                return false;
            int result = Convert.ToInt32(Math.Abs(a * ponto.X + b * ponto.Y + c) / (Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2))));
            return (result == 0);
        }
        public static bool VerificarSePontoPertenceAReta(Line linha, Point ponto)
        {
            double a, b, c;
            double y1 = linha.Point1.Y;
            double y2 = linha.Point2.Y;
            double x1 = linha.Point1.X;
            double x2 = linha.Point2.X;

            //Equação geral da reta =>  ax + by + c = 0
            a = y1 - y2;
            b = x2 - x1;
            c = (x1 - x2) * y1 + (y2 - y1) * x1;

            return VerificarSePontoPertenceAReta(a, b, c, ponto);
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
