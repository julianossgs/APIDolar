using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace CotacaoDolar
{
    public partial class FrmCotacaodolar : Form
    {
        public FrmCotacaodolar()
        {
            InitializeComponent();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            string URL= "https://api.hgbrasil.com/finance?array_limit=1&fields=only_results,USD&key=979355be";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = client.GetAsync(URL).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;

                        Market market = JsonConvert.DeserializeObject<Market>(result);

                        lblCompra.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", market.Currency.Buy);
                        lblVenda.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", market.Currency.Sell);
                        lblVariacao.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:P}", market.Currency.Variation/100);

                    }
                    else
                    {
                        lblCompra.Text = "-";
                        lblVariacao.Text = "-";
                        lblVenda.Text = "-";
                    }
                }
                catch (Exception ex)
                {
                    lblCompra.Text = "-";
                    lblVariacao.Text = "-";
                    lblVenda.Text = "-";
                    MessageBox.Show(ex.Message);
                }
                
            }
        }

        private void FrmCotacaodolar_Load(object sender, EventArgs e)
        {

            DateTime d1 = DateTime.Now;
          
            lblData.Text = d1.ToString("dd/MM/yyyy HH:mm");
        }

       
        private void btnLimpar_Click_1(object sender, EventArgs e)
        {
            lblCompra.Text = "0,0";
            lblVariacao.Text = "0,0";
            lblVenda.Text = "0,0";
        }
    }
}
