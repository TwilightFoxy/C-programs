using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int fact(int x)
        {
            return (x == 0) ? 1 : x * fact(x - 1);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string[] let = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "k" };
            int t = 0, n = 0, z = 0;
            if (richTextBox1.Text != "") t = 1;
            else if (richTextBox2.Text != "") t = 2;
            else if (richTextBox3.Text != "") t = 3;
            else if (richTextBox4.Text != "") t = 4;
            if (t == 1)
            {
                richTextBox2.Clear();
                richTextBox3.Clear();
                richTextBox4.Clear();
                string matSt = richTextBox1.Text;
                String[] str = matSt.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);//Разбиваем матрицу на строки
                n = str.Length;
                String[,] mass = new string[n, n];
                String[] pointsFF = matSt.Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);//Разбиваем матрицу на символы
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        mass[i, j] = pointsFF[z];
                        z++;

                    }
                }
                //Создаём матрицу смежности вершин
                for (int i = 0; i < n; i++)
                {
                    richTextBox4.AppendText(let[i] + ": ");
                    for (int j = 0; j < n; j++)
                    {
                        if (mass[i, j] == "1")
                            richTextBox4.AppendText(let[j] + " ");
                    }
                    richTextBox4.AppendText("\n");
                }
                //Список ребер по смежности вершин.
                int kolR = 0;
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (i < j && mass[i, j] == "1") kolR++;//Считаем сколько всего ребер
                    }
                }
                int[,] massR = new int[kolR, kolR];
                int nn = 0;
                nn = fact(n) / (fact(n - 2) * 2);//Сколько всего возможно ребер
                //richTextBox2.AppendText(kolR+ ":" +nn);
                string[] rebra = new string[nn];
                int zz = 0;
                for (int i = 0; i < n; i++)
                {
                    for (int j = i + 1; j < n; j++)
                    {
                        rebra[zz] = let[i] + let[j];
                        zz++;
                    }
                }
                string[,] smeshrbr = new string[nn + 1, nn + 1];
                for (int i = 0; i < nn + 1; i++)
                {
                    for (int j = 0; j < nn + 1; j++)
                    {
                        smeshrbr[i,j]="0";
                    }
                }
                for (int i = 1; i < nn + 1; i++)
                {
                    smeshrbr[0, i] = rebra[i-1];
                    smeshrbr[i, 0] = rebra[i-1];
                }//Закидываем названия всех возможных ребер в матрицу ответов

                for (int i = 1; i < nn + 1; i++)
                {
                    for (int j = 1; j < nn + 1; j++)
                    {
                        char[] arr1 = smeshrbr[i,0].ToCharArray();
                        char[] arr2 = smeshrbr[0,j].ToCharArray();
                        if (arr1[0] == arr2[0] || arr1[1] == arr2[1] || arr1[0] == arr2[1] || arr1[1] == arr2[0]) smeshrbr[i, j] = "1";
                    }
                }//Заполлняем матрицу еденицами в нужных местах

                

                string[,] smesVR = new string[n + 1, nn + 1];
                for (int i = 0; i < n + 1; i++)
                {
                    for (int j = 0; j < nn + 1; j++)
                    {
                        smesVR[i, j] = "0";
                    }
                }
                for (int i = 1; i < nn + 1; i++)
                    smesVR[0, i] = rebra[i - 1];
                for (int i = 1; i < n + 1; i++)
                    smesVR[i, 0] = let[i - 1];
                //Заполняем 0й столбец и строку


                for (int i = 1; i < n + 1; i++)
                {
                    for (int j = 1; j < nn + 1; j++)
                    {
                        char[] arr1 = smesVR[i, 0].ToCharArray();
                        char[] arr2 = smesVR[0, j].ToCharArray();
                        if (arr1[0] == arr2[0] || arr1[0] == arr2[1]) smesVR[i, j] = "1";
                    }
                }//Заполлняем матрицу еденицами в нужных местах

                


                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (mass[i, j] == "0")
                        {
                            string gg = let[i] + let[j];
                            for (int w = 0; w < nn+1; w++)
                            {
                                if (smeshrbr[0, w] == gg)
                                {
                                    for (int q = 1; q < nn+1; q++)
                                    {
                                        smeshrbr[q, w] = "0";
                                        smeshrbr[w, q] = "0";
                                    }//Исправляем вторую матрицу smeshrbr.
                                }
                            }
                            for (int w = 0; w < nn + 1; w++)
                            {
                                if (smesVR[0, w] == gg)
                                {
                                    for (int q = 1; q < nn + 1; q++)
                                    {
                                        smesVR[q, w] = "0";
                                    }//Исправляем третью матрицу smesVR.
                                }
                            }
                        }

                    }//mass[i,j]- матрица смежности вершин.
                }
                for (int i = 0; i < nn + 1; i++)
                {
                    for (int j = 0; j < nn + 1; j++)
                    {
                        if (i > 0 && j > 0) { richTextBox2.AppendText(smeshrbr[i, j] + "   "); }
                        else { richTextBox2.AppendText(smeshrbr[i, j] + " "); }
                    }
                    richTextBox2.AppendText("\n");
                }//вывод 2 матрицы

                for (int i = 0; i < n + 1; i++)
                {
                    for (int j = 0; j < nn + 1; j++)
                    {
                        if (i > 0 && j > 0) { richTextBox3.AppendText(smesVR[i, j] + "   "); }
                        else { richTextBox3.AppendText(smesVR[i, j] + " "); }
                    }
                    richTextBox3.AppendText("\n");
                }//вывод матрицы смежности вершин и ребер
                //Делаем проверку существования ребер и удаления их из матриц, если требуется
            }//Если заполнен 1 текстбокс
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
