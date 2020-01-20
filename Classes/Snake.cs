using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoDaCobrinhaCS.Classes {
    public class Snake {
        private Rectangle[] _snakeRetangulo;
        private SolidBrush Pincel;
        private int x, y, altura, largura;

        public enum Direcao {
            ESQUERDA,
            DIREITA,
            BAIXO,
            CIMA,
        };

        public Rectangle[] SnakeRetangulo {
            get {
                return _snakeRetangulo;
            }
        }

        public Snake() {
            _snakeRetangulo = new Rectangle[3];
            Pincel = new SolidBrush(Color.Blue);

            x = 30;
            y = 10;

            largura = 10;
            altura = 10;

            for (int i = 0; i < _snakeRetangulo.Length; i++) {
                _snakeRetangulo[i] = new Rectangle(x, y, largura, altura);
                x -= 10;
            }
        }

        public void DesenharSnake(Graphics graficos) {
            foreach (Rectangle retangulo in _snakeRetangulo) {
                graficos.FillRectangle(Pincel, retangulo);
            }
        }

        public void DesenharSnake () {
            for (int i = _snakeRetangulo.Length - 1; i > 0; i--) {
                _snakeRetangulo[i] = _snakeRetangulo[i - 1];
            }
        }

        public void MovimentarSnake(Direcao direcao) {
            DesenharSnake();

            switch (direcao) {
                case Direcao.ESQUERDA: {
                        _snakeRetangulo[0].X -= 10;
                    }
                    break;
                case Direcao.DIREITA: {
                        _snakeRetangulo[0].X += 10;
                    }
                    break;
                case Direcao.BAIXO: {
                        _snakeRetangulo[0].Y += 10;
                    }
                    break;
                case Direcao.CIMA: {
                        _snakeRetangulo[0].Y -= 10;
                    }
                    break;
                default:
                    break;
            }
        }

        public void AlimentarSnake() {
            List<Rectangle> Retangulos = _snakeRetangulo.ToList();

            Retangulos.Add(new Rectangle(_snakeRetangulo[_snakeRetangulo.Length - 1].X,
                                         _snakeRetangulo[_snakeRetangulo.Length - 1].Y,
                                         _snakeRetangulo[_snakeRetangulo.Length - 1].Width,
                                         _snakeRetangulo[_snakeRetangulo.Length - 1].Height));

            _snakeRetangulo = Retangulos.ToArray();
        }
    }
}
