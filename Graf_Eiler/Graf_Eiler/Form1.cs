using Elasticsearch.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graf_Eiler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label5.Text = "Внимание! Работает только\nдля графов из 5 и мение вершин!";
            label4.Text = "Максимальная клика: ";
        }
        /*
        public class Graf
        {
            public struct Vertex
            {
                string date;

                public Vertex(string new_date)
                {
                    this.date = new_date;
                }
                public void setVertexData(string new_date)
                {
                    this.date = new_date;
                }
                public string getVertexData()
                {
                    return this.date;
                }
            }
            public struct Edge
            {
                Vertex stockVertex;
                Vertex sourceVertex;
                string inf;
                public Edge(Vertex new_stockVertex, Vertex new_sourceVertex)
                {
                    this.stockVertex = new_stockVertex;
                    this.sourceVertex = new_sourceVertex;
                    this.inf = null;
                }
                public Edge(Vertex new_stockVertex, Vertex new_sourceVertex, string new_inf)
                {
                    this.stockVertex = new_stockVertex;
                    this.sourceVertex = new_sourceVertex;
                    this.inf = new_inf;
                }
                public void setStockVertex(Vertex new_stockVertex)
                {
                    this.stockVertex = new_stockVertex;
                }
                public Vertex getStockVertex()
                {
                    return this.stockVertex;
                }
                public void setSourceVertex(Vertex new_sourceVertex)
                {
                    this.sourceVertex = new_sourceVertex;
                }
                public Vertex getSourceVertex()
                {
                    return this.sourceVertex;
                }
                public void setInf(string new_inf)
                {
                    this.inf = new_inf;
                }
                public string getInf()
                {
                    return this.inf;
                }
            }
            List<Vertex> list_vertex;
            List<Edge> list_edge;

            public void add_to_edge_list(Edge edge_to_add)
            {
                list_edge.Add(edge_to_add);
            }
            public void del_from_edge_list(string inf_to_del)
            {
                foreach (Edge e in list_edge)
                {
                    if (e.getInf() == inf_to_del)
                    {
                        list_edge.Remove(e);
                    }
                }
            }
            public void add_to_vertex_list(Vertex vertex_to_add)
            {
                list_vertex.Add(vertex_to_add);
            }
            public void del_from_vertex_list(string date_to_del)
            {
                foreach (Vertex v in list_vertex)
                {
                    if (v.getVertexData() == date_to_del)
                    {
                        list_vertex.Remove(v);
                    }
                }
            }
            //Описать для стока и истока
        }
    */
        private void button1_Click(object sender, EventArgs e)
        {
            string matSt = richTextBox1.Text;
            String[] str = matSt.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);//Разбиваем матрицу на строки
            int n = str.Length;
            int z = 0;
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
            int[] prov_mass = new int[n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (mass[i, j] != "0")
                        prov_mass[i]++;
                }
            }
            int chetchik = 0;
            for (int i = 0; i < n; i++)
            {
                if (prov_mass[i]%2==1)
                    chetchik++;
            }
            if (chetchik==2)
                label2.Text = "Граф является\nполуэйлеровым!";
            else
                label2.Text = "Граф НЕ является\nполуэйлеровым!";
            //MessageBox.Show("Граф является полуэйлеровым!");
        }
        #region Всё для Брона-Кербоша(пожалуйста, убейте меня)
        void otvet(int a, int b, int c, int d, int e)
        {
            label4.Text = "Максимальная клика: " + a + "-" + b + "-" + c + "-" + d + "-" + e;
        }
        void otvet(int a, int b, int c, int d)
        {
            label4.Text = "Максимальная клика: " + a + "-" + b + "-" + c + "-" + d;
        }
        void otvet(int a, int b, int c)
        {
            label4.Text = "Максимальная клика: " + a + "-" + b + "-" + c;
        }
        void otvet(int a, int b)
        {
            label4.Text = "Максимальная клика: " + a + "-" + b;
        }
        bool prov_function(int[,] a, int n)
        {
            int prov = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (a[i, j] ==1) prov +=1; ;
                }
            }
            if (prov==n*(n-1)) return true;
            else return false;
        }
        void sim_matr(int[,] x, int n)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i > j) x[i, j] = x[j, i];
                }
            }
        }
        bool swap5(int[,] arr, int a, int b, int c, int d)
        {
            int n = 4;
            int[,] x = new int[n, n];
            x[0, 1] = arr[a, b];
            x[0, 2] = arr[a, c];
            x[0, 3] = arr[a, d];
            x[1, 0] = arr[b, a];
            x[1, 2] = arr[b, c];
            x[1, 3] = arr[b, d];
            x[2, 3] = arr[c, d];
            sim_matr(arr, n);
            if (prov_function(arr, n))
            {
                otvet(a, b, c, d);
                return true;
            }
            else if (swap4(x, a, b, c))
            {
                otvet(a, b, c);
                return true;
            }
            else if (swap4(arr, a, b, d))
            {
                otvet(a, b, d);
                return true;
            }
            else if (swap4(arr, a, c, d))
            {
                otvet(a, c, d);
                return true;
            }
            else if (swap4(arr, b, c, d))
            {
                otvet(b, c, d);
                return true;
            }
            return false;
        }
        bool swap4(int[,] arr, int a, int b, int c)
        {
            int n = 3;
            if (a > n) a -= n;
            if (b > n) b -= n;
            if (c > n) c -= n;
            int[,] x = new int[n, n];
            x[0, 1] = arr[a, b];
            x[0, 2] = arr[a, c];
            x[1, 2] = arr[b, c];
            sim_matr(arr, n);
            if (prov_function(arr, n))
            {
                otvet(a, b, c);
                return true;
            }
            else if (swap3(x, a, b))
            {
                otvet(a, b);
                return true;
            }
            else if (swap3(x, a, c))
            {
                otvet(a, 2);
                return true;
            }
            else if (swap3(x, b, c))
            {
                otvet(b, c);
                return true;
            }
            return false;
        }
        bool swap3(int[,] arr, int a, int b)
        {
            int n = 2;
            if (a > n) a -= n;
            if (b > n) b -= n;
            int[,] x = new int[n, n];
            x[0, 1] = arr[a, b];
            x[1, 0] = arr[b, a];
            return prov_function(x, n);
        }
        #endregion
        private void button2_Click(object sender, EventArgs e)
        {
            string matSt = richTextBox2.Text;
            String[] str = matSt.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);//Разбиваем матрицу на строки
            int n = str.Length;
            int z = 0;
            String[,] mass = new string[n, n];
            String[] pointsFF = matSt.Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);//Разбиваем матрицу на символы
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    mass[i, j] = (pointsFF[z]);
                    z++;
                }
            }
            int[,] a = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (mass[i, j] == "1") a[i, j] = 1;
                    else a[i, j] = 0;
                }
            }
            /////////////////////////////////////////////////////////////
            if (prov_function(a, n) && n == 5) otvet(0, 1, 2, 3, 4);
            else if (prov_function(a, n) && n == 4) otvet(0, 1, 2, 3);
            else if (prov_function(a, n) && n == 3) otvet(0, 1, 2);
            /////////////////////////////////////////////////////////////
            ///swap5
            else if (swap5(a, 0, 1, 2, 3) && n >= 5) otvet(0, 1, 2, 3);
            else if (swap5(a, 0, 1, 2, 4) && n >= 5) otvet(0, 1, 2, 4);
            else if (swap5(a, 0, 1, 3, 4) && n >= 5) otvet(0, 1, 3, 4);
            else if (swap5(a, 0, 2, 3, 4) && n >= 5) otvet(0, 2, 3, 4);
            else if (swap5(a, 1, 2, 3, 4) && n >= 5) otvet(1, 2, 3, 4);
            else if (swap4(a, 0, 1, 2) && n >= 4) otvet(0, 1, 2);
            else if (swap4(a, 0, 1, 3) && n >= 4) otvet(0, 1, 3);
            else if (swap4(a, 0, 1, 4) && n >= 5) otvet(0, 1, 4);
            else if (swap4(a, 0, 2, 3) && n >= 4) otvet(0, 2, 3);
            else if (swap4(a, 0, 2, 4) && n >= 5) otvet(0, 2, 4);
            else if (swap4(a, 0, 3, 4) && n >= 5) otvet(0, 3, 4);
            else if (swap4(a, 1, 2, 3) && n >= 4) otvet(1, 2, 3);
            else if (swap4(a, 1, 2, 4) && n >= 5) otvet(1, 2, 4);
            else if (swap4(a, 1, 3, 4) && n >= 5) otvet(1, 3, 4);
            else
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (i != j && a[i, j] == 1) otvet(i, j);
                    }
                }
            }
            /////////////////////////////////////////////////////////////
        }

    }
}
