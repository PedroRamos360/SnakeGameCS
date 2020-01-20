using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoDaCobrinhaCS.Classes {
    public class Food {
        Rectangle _comidaRetangulo;
        SolidBrush Pincel;
        int x, y, largura, altura;

        public Rectangle ComidaRetangulo { // para acessar a váriavel de um arquivo externo
            get {
                return _comidaRetangulo;
            }
        }

        public Food(Random random) {
            ComidaPosicao(random);

            Pincel = new SolidBrush(Color.LimeGreen);

            largura = 10;
            altura = 10;

            _comidaRetangulo = new Rectangle(x, y, largura, altura);
        }

        public void ComidaPosicao(Random random) {
            x = random.Next(1, 39) * 10;
            y = random.Next(1, 39) * 10;
        }

        public void ComidaDesenhar(Graphics graficos) {
            _comidaRetangulo.X = x;
            _comidaRetangulo.Y = y;

            graficos.FillRectangle(Pincel, _comidaRetangulo);
        }
    }
}
