using JogoDaCobrinhaCS.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JogoDaCobrinhaCS {
    public partial class Form1 : Form {
        Snake snake = new Snake();
        Board tabuleiro = new Board();
        Food comida;

        int pontos = 0;
        public void GameOver() { // Cria tela de Game Over e reinicia o jogo
            timer1.Enabled = false;

            MessageBox.Show("FIM DE JOGO");

            if (GerenciadorArquivoIni.PontuacaoMaxima(pontos)) {
                MessageBox.Show($"Parabéns, você conquistou um novo recorde de {pontos} pontos");
                lblRecorde.Text = String.Format($"Recorde: {pontos}");
            }

            GerenciadorArquivoIni.Salvar(pontos, tabuleiro.Colidir);

            timer1.Enabled = true;
            snake = new Snake();

            pontos = 0;

            ESQUERDA = false;
            DIREITA = false;
            BAIXO = false;
            CIMA = false;

            comida.ComidaPosicao(random);
        }

        public void DetectarColisao() {
            #region Auto Colisão
            for (int i = 1; i < snake.SnakeRetangulo.Length; i++) { // i = para desconsiderar a cabeça
                if (snake.SnakeRetangulo[0].IntersectsWith(snake.SnakeRetangulo[i])) {
                    GameOver();
                }
            }
            #endregion

            #region Colisão Com As Bordas
            switch (tabuleiro.Colidir) {
                case Board.ColidirBordas.Nao:
                    #region NÃO
                    for (int i = 0; i < snake.SnakeRetangulo.Length; i++) {
                        // Colisão lado esquerdo
                        if (snake.SnakeRetangulo[i].X < tabuleiro.TabuleiroRetangulo.Left) {
                            snake.SnakeRetangulo[i].X = tabuleiro.TabuleiroRetangulo.Right - snake.SnakeRetangulo[i].Width;
                        }

                        // Colisão lado direito
                        if (snake.SnakeRetangulo[i].X > tabuleiro.TabuleiroRetangulo.Right - 10) {
                            snake.SnakeRetangulo[i].X = tabuleiro.TabuleiroRetangulo.Left;
                        }

                        // Colisão com topo
                        if (snake.SnakeRetangulo[i].Y < tabuleiro.TabuleiroRetangulo.Top) {
                            snake.SnakeRetangulo[i].Y = tabuleiro.TabuleiroRetangulo.Bottom - snake.SnakeRetangulo[i].Height;
                        }

                        // Colisão com o chão
                        if (snake.SnakeRetangulo[i].Y > tabuleiro.TabuleiroRetangulo.Bottom - snake.SnakeRetangulo[i].Height) {
                            snake.SnakeRetangulo[i].Y = tabuleiro.TabuleiroRetangulo.Top;
                        }
                    }
                    #endregion
                    break;
                case Board.ColidirBordas.Sim:
                    #region SIM
                    // Colisão Horizontal
                    if (snake.SnakeRetangulo[0].X < tabuleiro.TabuleiroRetangulo.Left||
                        snake.SnakeRetangulo[0].X > (tabuleiro.TabuleiroRetangulo.Right - snake.SnakeRetangulo[0].Width)) {
                        GameOver();
                    }

                    // Colisão Vertical
                    if (snake.SnakeRetangulo[0].Y < (tabuleiro.TabuleiroRetangulo.Top) ||
                        snake.SnakeRetangulo[0].Y > (tabuleiro.TabuleiroRetangulo.Bottom - snake.SnakeRetangulo[0].Height)) {
                        GameOver();
                    }
                    #endregion
                    break;
                default:
                    break;
            }
            #endregion
        }

        Random random = new Random();

        bool ESQUERDA = false;
        bool DIREITA = false;
        bool CIMA = false;
        bool BAIXO = false;
        public Form1() {
            InitializeComponent();

            lblRecorde.Text = String.Format($"Recorde: {int.Parse(GerenciadorArquivoIni.ini.Read("pontosmax", "SnakeGame"))}");

            comida = new Food(random);

            GerenciadorArquivoIni.CriarSeNaoExistir();

            tabuleiro.Colidir = GerenciadorArquivoIni.GetColidir();
        }

        private void Lbl_Pontos_Click(object sender, EventArgs e) {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e) {
            switch (e.KeyCode) {
                case Keys.Left: {
                        if (!DIREITA) {
                            ESQUERDA = true;
                            DIREITA = false;
                            CIMA = false;
                            BAIXO = false;
                        }
                    }
                    break;
                case Keys.Right: {
                        if (!ESQUERDA) {
                            ESQUERDA = false;
                            DIREITA = true;
                            CIMA = false;
                            BAIXO = false;
                        }
                    }
                    break;
                case Keys.Down: {
                        if (!CIMA) {
                            ESQUERDA = false;
                            DIREITA = false;
                            CIMA = false;
                            BAIXO = true;
                        }
                    }
                    break;
                case Keys.Up: {
                        if (!BAIXO) {
                            ESQUERDA = false;
                            DIREITA = false;
                            CIMA = true;
                            BAIXO = false;
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void Timer1_Tick(object sender, EventArgs e) {
            if (ESQUERDA) {
                snake.MovimentarSnake(Snake.Direcao.ESQUERDA);
            }
            if (DIREITA) {
                snake.MovimentarSnake(Snake.Direcao.DIREITA);
            }
            if (BAIXO) {
                snake.MovimentarSnake(Snake.Direcao.BAIXO);
            }
            if (CIMA) {
                snake.MovimentarSnake(Snake.Direcao.CIMA);
            }

            this.Invalidate();

            if (snake.SnakeRetangulo[0].IntersectsWith(comida.ComidaRetangulo)) {
                pontos++;
                snake.AlimentarSnake();
                comida.ComidaPosicao(random);
            }

            DetectarColisao();

            toolStripStatusLabel1.Text = String.Format($"Pontos: {pontos}");
            this.Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e) {
            tabuleiro.BoardDesenhar(e.Graphics);
            snake.DesenharSnake(e.Graphics);
            comida.ComidaDesenhar(e.Graphics);
        }

        private void ToolStripStatusLabel2_Click(object sender, EventArgs e) {

        }
    }
}
