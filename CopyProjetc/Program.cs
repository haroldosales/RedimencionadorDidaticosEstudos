using System.Threading;
using System;
using System.Drawing;

internal class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Iniciando redimenciosna");



        Thread thread = new Thread(Redimencionar);

        thread.Start();

        Console.Read();




    }
    static void Redimencionar()
    {
        #region "Diretorios"
        string diretorioEntrada = "arquivo_entrada";
        string diretorioRedimencionado = "arquivo_redimencionado";
        string diretorioFinalizado = "arquivo_finalizado";
        if (!Directory.Exists(diretorioEntrada))
        {
            Console.WriteLine("Criado com sucesso");
            Directory.CreateDirectory(diretorioEntrada);
        }

        if (!Directory.Exists(diretorioRedimencionado))
        {
            Console.WriteLine("Criado com sucesso");

            Directory.CreateDirectory(diretorioRedimencionado);
        }

        if (!Directory.Exists(diretorioFinalizado))
        {
            Console.WriteLine("Criado com sucesso");

            Directory.CreateDirectory(diretorioFinalizado);
        }
        #endregion

        FileStream fileStream;
        FileInfo fileInfo;
        while (true)
        {

            var arquivosEntrada = Directory.EnumerateFiles(diretorioEntrada);
            int novaAltura = 200;
            foreach (var arquivo in arquivosEntrada)
            {
                fileStream = new FileStream(arquivo, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                fileInfo = new FileInfo(arquivo);

                string caminho = Environment.CurrentDirectory + @"\"
                        + diretorioRedimencionado + @"\" + fileInfo.Name + DateTime.Now.Millisecond.ToString();

                Red(Image.FromStream(fileStream), novaAltura, caminho);

                fileStream.Close();
                string caminhoF = Environment.CurrentDirectory + @"\"
                        + diretorioFinalizado + @"\" + fileInfo.Name + DateTime.Now.Millisecond.ToString();


                fileInfo.MoveTo(caminhoF);
            }


            Console.WriteLine("Estou vivo");
            Thread.Sleep(new TimeSpan(0, 0, 3));
        }
    }

    static void Red(Image imagem, int altura, string caminho)
    {
        double ratio = (double)altura / (double)imagem.Height;
        int novaLagura = (int)(imagem.Width * ratio);
        int novaAltura = (int)(imagem.Height * ratio);

        Bitmap novaImage = new Bitmap(novaLagura, novaAltura);

        using (Graphics g = Graphics.FromImage(novaImage))
        {
            g.DrawImage(imagem, 0, 0, novaLagura, novaAltura);


        }

        novaImage.Save(caminho);
        imagem.Dispose();

    }
}