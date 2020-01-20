using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoDaCobrinhaCS.Classes {
    class GerenciadorArquivoIni {
        public static ArquivoIni ini = new ArquivoIni("Cfg.ini");

        public static void CriarSeNaoExistir() {
            if (!File.Exists(ini.Path)) {
                ini.Write("pontosmax", "0", "SnakeGame");
                ini.Write("colidir", "0", "SnakeGame");
            }
        }

        public static void Salvar(int pontos, Board.ColidirBordas colidir) {
            if (PontuacaoMaxima(pontos)) {
                ini.Write("pontosmax", pontos.ToString(), "SnakeGame");
            }

            ini.Write("colidir", ((int)colidir).ToString(), "SnakeGame");
        }

        public static bool PontuacaoMaxima(int pontos) {
            return pontos > int.Parse(ini.Read("pontosmax", "SnakeGame")); // True ou False
        }

        public static Board.ColidirBordas GetColidir() {
            return (Board.ColidirBordas)int.Parse(ini.Read("colidir", "SnakeGame"));
        }
    }
}
