using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoDaCobrinhaCS.Classes {
    public class Board {
        Rectangle _tabuleiroRetangulo;
        SolidBrush Pincel;
        int x, y, largura, altura;

        public ColidirBordas Colidir;

        public enum ColidirBordas {
            Nao = 0,
            Sim = 1,
        };

        public Rectangle TabuleiroRetangulo {
            get {
                return _tabuleiroRetangulo;
            }
        }

        public Board() {
            Pincel = new SolidBrush(Color.Black);

            x = 10;
            y = 10;
            largura = 400;
            altura = 400;

            _tabuleiroRetangulo = new Rectangle(x, y, largura, altura);
        }

        public void BoardDesenhar(Graphics graficos) {
            _tabuleiroRetangulo.X = x;
            _tabuleiroRetangulo.Y = y;

            graficos.FillRectangle(Pincel, _tabuleiroRetangulo);
        }
    }
}
