using System;
using System.Configuration;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.IO.Packaging;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Ink;
using System.Windows.Shapes;
using System.Windows.Xps;
using System.Xml;
using AplicativoVidracariaPopular;
using GemBox.Pdf;
using OfficeOpenXml;
using Spire.Doc;
using Xceed.Document.NET;
using Xceed.Words.NET;
using PdfiumViewer;

namespace AplicativoVidracariaPopular
{
    public partial class Form1 : Form
    {
        FormQuadro[] formQuadros = [new(), new(), new(), new(), new(), new(), new(), new(), new(), new()];
        public Form1()
        {
            InitializeComponent();


            foreach (var formQuadro in formQuadros)
            {
                formQuadro.Dock = DockStyle.Fill;
                formQuadro.TopLevel = false;
                formQuadro.TopMost = true;
                formQuadro.FormBorderStyle = FormBorderStyle.None;
            }

            this.panelFormLoader.Controls.Clear();
            FormBase formBase = new()
            {
                Dock = DockStyle.Fill,
                TopLevel = false,
                TopMost = true,
                FormBorderStyle = FormBorderStyle.None
            };
            this.panelFormLoader.Controls.Add(formBase);
            formBase.Show();

            Program.valorVidro = excel(1, 3) / 10000;
            Program.valorVidroAR = excel(2, 3) / 10000;
            Program.valorMdf = excel(3, 3) / 10000;
            Program.valorEspelho = excel(4, 3) / 10000;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DALOrcamento.CriarBancoSQLite();
            DALOrcamento.CriarTabelaSQLite();
            DALClientes.CriarBancoSQLite();
            DALClientes.CriarTabelaSQLite();
            DALClientes.CriarLista();
            atualizarListaClientes();
        }

        private void buttonPronto_Click(object sender, EventArgs e)
        {
            double valorTotal = 0;
            double valorUnitario = 0;
            double valorTabela;

            if (textBoxNome.Text == "")
            {
                MessageBox.Show("N�o Deixe Valores Zerados", "Valor Zerado Nome",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (textBoxTelefone.Text == "")
            {
                MessageBox.Show("N�o Deixe Valores Zerados", "Valor Zerado Telefone",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (textBoxEndereco.Text == "")
            {
                MessageBox.Show("N�o Deixe Valores Zerados", "Valor Zerado Endere�o",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (textBoxUsuario.Text == "")
            {
                MessageBox.Show("N�o Deixe Valores Zerados", "Valor Zerado Usu�rio",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



            Button[] buttonArray = [ buttonQuadro1, buttonQuadro2, buttonQuadro3, buttonQuadro4, buttonQuadro5, buttonQuadro6,
                buttonQuadro7, buttonQuadro8, buttonQuadro9, buttonQuadro10 ];

            for (int i = 0; i < 10; i++)
            {
                if (buttonArray[i].BackColor.Equals(Color.FromArgb(46, 51, 73)))
                {
                    Program.quadroExistente[i] = true;
                    if (formQuadros[i].checkBoxMoldura1.Checked)
                    {
                        if (formQuadros[i].textBoxMolduraTipo1_2.Text != "0000")
                            valorTabela = excel(i, 1);
                        else
                            valorTabela = excel(i, 2);

                        if (formQuadros[i].numericUpDownMoldura1.Value == 0 | formQuadros[i].numericUpDownMoldura1_2.Value == 0)
                            MessageBox.Show("N�o Deixe Valores Zerados", "Valor Zerado Tamanho 1",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else if (formQuadros[i].numericUpDownQuantidade.Value == 0)
                            MessageBox.Show("N�o Deixe Valores Zerados", "Valor Zerado Quantidade 1",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else if (formQuadros[i].textBoxDescricao.Text == "")
                            MessageBox.Show("N�o Deixe Valores Zerados", "Valor Zerado Descri��o 1",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else if (valorTabela == 0)
                            MessageBox.Show("Tipo de Moldura 1 n�o Reconhecida", "Erro na escolha de Moldura 1",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                        {
                            Program.moldura1[i] = formQuadros[i].checkBoxMoldura1.Checked;
                            Program.moldura1Tipo1[i] = formQuadros[i].textBoxMolduraTipo1.Text;
                            Program.moldura1Tipo2[i] = formQuadros[i].textBoxMolduraTipo1_2.Text;
                            Program.molduraTamanho1[i] = Convert.ToDouble(formQuadros[i].numericUpDownMoldura1.Value);
                            Program.molduraTamanho2[i] = Convert.ToDouble(formQuadros[i].numericUpDownMoldura1_2.Value);
                            Program.molduraExtra[i] = Convert.ToDouble(formQuadros[i].numericUpDownMoldura1_3.Value);
                            Program.quantidade[i] = Convert.ToDouble(formQuadros[i].numericUpDownQuantidade.Value);
                            Program.descricao[i] = formQuadros[i].textBoxDescricao.Text;
                            valorUnitario += (Program.molduraTamanho1[0] * 2 + Program.molduraTamanho2[0] * 2 + Program.molduraExtra[0]) * valorTabela / 100;
                        }
                    }

                    if (formQuadros[i].checkBoxMoldura2.Checked)
                    {
                        if (formQuadros[i].textBoxMolduraTipo1_2.Text != "0000")
                            valorTabela = excel(i, 1);
                        else
                            valorTabela = excel(i, 2);

                        if (valorTabela == 0)
                            MessageBox.Show("Tipo de Moldura 2 n�o Reconhecida", "Erro na escolha de Moldura 2",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                        {
                            Program.moldura2[i] = formQuadros[i].checkBoxMoldura2.Checked;
                            Program.moldura2Tipo1[i] = formQuadros[i].textBoxMolduraTipo2.Text;
                            Program.moldura2Tipo2[i] = formQuadros[i].textBoxMolduraTipo2_2.Text;
                            valorUnitario += (Program.molduraTamanho1[i] * 2 + Program.molduraTamanho2[i] * 2) * valorTabela / 100;
                        }
                    }

                    if (formQuadros[i].checkBoxMoldura3.Checked)
                    {
                        if (formQuadros[i].textBoxMolduraTipo1_2.Text != "0000")
                            valorTabela = excel(i, 1);
                        else
                            valorTabela = excel(i, 2);

                        if (valorTabela == 0)
                            MessageBox.Show("Tipo de Moldura 3 n�o Reconhecida", "Erro na escolha de Moldura 3",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                        {
                            Program.moldura3[i] = formQuadros[i].checkBoxMoldura3.Checked;
                            Program.moldura3Tipo1[i] = formQuadros[i].textBoxMolduraTipo3.Text;
                            Program.moldura3Tipo2[i] = formQuadros[i].textBoxMolduraTipo3_2.Text;
                            valorUnitario += (Program.molduraTamanho1[i] * 2 + Program.molduraTamanho2[i] * 2) * valorTabela / 100;
                        }
                    }

                    if (formQuadros[i].checkBoxVidro.Checked)
                    {
                        if (formQuadros[i].numericUpDownMoldura1.Value == 0 | formQuadros[i].numericUpDownMoldura1_2.Value == 0)
                            MessageBox.Show("N�o Deixe Valores Zerados", "Valor Zerado Tamanho 1",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else if (formQuadros[i].numericUpDownQuantidade.Value == 0)
                            MessageBox.Show("N�o Deixe Valores Zerados", "Valor Zerado Quantidade 1",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else if (formQuadros[i].textBoxDescricao.Text == "")
                            MessageBox.Show("N�o Deixe Valores Zerados", "Valor Zerado Descri��o 1",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                        {
                            Program.vidro[i] = formQuadros[i].checkBoxVidro.Checked;
                            Program.molduraTamanho1[i] = Convert.ToDouble(formQuadros[i].numericUpDownMoldura1.Value);
                            Program.molduraTamanho2[i] = Convert.ToDouble(formQuadros[i].numericUpDownMoldura1_2.Value);
                            Program.quantidade[i] = Convert.ToDouble(formQuadros[i].numericUpDownQuantidade.Value);
                            Program.descricao[i] = formQuadros[i].textBoxDescricao.Text;
                            valorUnitario += (Program.molduraTamanho1[i] * Program.molduraTamanho2[i]) * Program.valorVidro;
                        }
                        if (formQuadros[i].checkBoxVidro2o.Checked)
                        {
                            Program.vidro2o[i] = formQuadros[i].checkBoxVidro2o.Checked;
                            valorUnitario += (Program.molduraTamanho1[i] * Program.molduraTamanho2[i]) * Program.valorVidro;
                        }
                    }

                    if (formQuadros[i].checkBoxVidroAR.Checked)
                    {
                        if (formQuadros[i].numericUpDownMoldura1.Value == 0 | formQuadros[i].numericUpDownMoldura1_2.Value == 0)
                            MessageBox.Show("N�o Deixe Valores Zerados", "Valor Zerado Tamanho 1",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else if (formQuadros[i].numericUpDownQuantidade.Value == 0)
                            MessageBox.Show("N�o Deixe Valores Zerados", "Valor Zerado Quantidade 1",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else if (formQuadros[i].textBoxDescricao.Text == "")
                            MessageBox.Show("N�o Deixe Valores Zerados", "Valor Zerado Descri��o 1",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                        {
                            Program.vidroAR[i] = formQuadros[i].checkBoxVidroAR.Checked;
                            Program.molduraTamanho1[i] = Convert.ToDouble(formQuadros[i].numericUpDownMoldura1.Value);
                            Program.molduraTamanho2[i] = Convert.ToDouble(formQuadros[i].numericUpDownMoldura1_2.Value);
                            Program.quantidade[i] = Convert.ToDouble(formQuadros[i].numericUpDownQuantidade.Value);
                            Program.descricao[i] = formQuadros[i].textBoxDescricao.Text;
                            valorUnitario += (Program.molduraTamanho1[i] * Program.molduraTamanho2[i]) * Program.valorVidroAR;
                        }
                        if (formQuadros[i].checkBoxVidroAR2o.Checked)
                        {
                            Program.vidroAR2o[i] = formQuadros[i].checkBoxVidroAR2o.Checked;
                            valorUnitario += (Program.molduraTamanho1[i] * Program.molduraTamanho2[i]) * Program.valorVidroAR;
                        }
                    }

                    if (formQuadros[i].checkBoxEspelho.Checked)
                    {
                        if (formQuadros[i].numericUpDownMoldura1.Value == 0 | formQuadros[i].numericUpDownMoldura1_2.Value == 0)
                            MessageBox.Show("N�o Deixe Valores Zerados", "Valor Zerado Tamanho 1",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else if (formQuadros[i].numericUpDownQuantidade.Value == 0)
                            MessageBox.Show("N�o Deixe Valores Zerados", "Valor Zerado Quantidade 1",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else if (formQuadros[i].textBoxDescricao.Text == "")
                            MessageBox.Show("N�o Deixe Valores Zerados", "Valor Zerado Descri��o 1",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                        {
                            Program.espelho[i] = formQuadros[i].checkBoxEspelho.Checked;
                            Program.molduraTamanho1[i] = Convert.ToDouble(formQuadros[i].numericUpDownMoldura1.Value);
                            Program.molduraTamanho2[i] = Convert.ToDouble(formQuadros[i].numericUpDownMoldura1_2.Value);
                            Program.quantidade[i] = Convert.ToDouble(formQuadros[i].numericUpDownQuantidade.Value);
                            Program.descricao[i] = formQuadros[i].textBoxDescricao.Text;
                            valorUnitario += (Program.molduraTamanho1[i] * Program.molduraTamanho2[i]) * Program.valorEspelho;
                        }
                    }

                    if (formQuadros[i].checkBoxMdf.Checked)
                    {
                        if (formQuadros[i].numericUpDownMoldura1.Value == 0 | formQuadros[i].numericUpDownMoldura1_2.Value == 0)
                            MessageBox.Show("N�o Deixe Valores Zerados", "Valor Zerado Tamanho 1",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else if (formQuadros[i].textBoxDescricao.Text == "")
                            MessageBox.Show("N�o Deixe Valores Zerados", "Valor Zerado Descri��o 1",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                        {
                            Program.mdf[i] = formQuadros[i].checkBoxMdf.Checked;
                            Program.molduraTamanho1[i] = Convert.ToDouble(formQuadros[i].numericUpDownMoldura1.Value);
                            Program.molduraTamanho2[i] = Convert.ToDouble(formQuadros[i].numericUpDownMoldura1_2.Value);
                            Program.quantidade[i] = Convert.ToDouble(formQuadros[i].numericUpDownQuantidade.Value);
                            Program.descricao[i] = formQuadros[i].textBoxDescricao.Text;
                            valorUnitario += (Program.molduraTamanho1[i] * Program.molduraTamanho2[i]) * Program.valorMdf;
                        }

                    }

                    if (formQuadros[i].checkBoxPaspatur.Checked)
                    {
                        if (formQuadros[i].numericUpDownPaspaturTamanho.Value == 0 | formQuadros[i].numericUpDownPaspaturValor.Value == 0)
                            MessageBox.Show("N�o Deixe Valores Zerados", "Valor Zerado Paspatur",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                        {
                            Program.paspatur[i] = formQuadros[i].checkBoxPaspatur.Checked;
                            Program.paspaturTamanho[i] = Convert.ToDouble(formQuadros[i].numericUpDownPaspaturTamanho.Value);
                            Program.paspaturValor[i] = Convert.ToDouble(formQuadros[i].numericUpDownPaspaturValor.Value);
                            Program.descricao[i] = formQuadros[i].textBoxDescricao.Text;
                            Program.quantidade[i] = Convert.ToDouble(formQuadros[i].numericUpDownQuantidade.Value);
                            valorUnitario += Program.paspaturValor[i];
                        }
                    }

                    if (formQuadros[i].checkBoxMolduraExtra.Checked)
                    {
                        if (formQuadros[i].numericUpDownExtra.Value == 0)
                            MessageBox.Show("N�o Deixe Valores Zerados", "Valor Zerado Extra",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                        {
                            Program.extra[i] = formQuadros[i].checkBoxMolduraExtra.Checked;
                            Program.extraValor[i] = Convert.ToDouble(formQuadros[i].numericUpDownExtra.Value);
                            Program.descricao[i] = formQuadros[i].textBoxDescricao.Text;
                            valorUnitario += Program.extraValor[i];
                        }
                    }
                    valorTotal = valorUnitario * Program.quantidade[i];

                    Program.valorUnitario[i] = valorUnitario;
                    Program.valorTotal[i] = valorTotal;

                    Program.valorTotalGeral = 0;
                    foreach (double valor in Program.valorTotal)
                    {
                        Program.valorTotalGeral += valor;
                    }

                    labelValorTotal.Text = (Convert.ToDecimal(Program.valorTotalGeral) - numericUpDownDesconto.Value - numericUpDownSinal.Value).ToString("0.00");
                    labelValorTotal.Refresh();

                    formQuadros[i].labelValorUnitario.Text = Program.valorUnitario[i].ToString("0.00");
                }
            }
        }

        private void buttonImprimir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Imprimir?", "Impress�o", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DALOrcamento.Add(textBoxNome.Text);
                int id = DALOrcamento.GetId();

                int rowCount = 0;
                foreach (var qE in Program.quadroExistente)
                {
                    if (qE)
                        rowCount++;
                }


                word(id, rowCount);

                string pdfName = id + " - " + textBoxNome.Text + ".pdf";
                string pdfLocation = @$"documentosGerados\" + id + " - " + textBoxNome.Text + ".pdf";

                using (var doc = new Spire.Doc.Document())
                {
                    doc.LoadFromFile(@"documentos\papelTimbradoGerado.docx");
                    doc.SaveToFile(pdfLocation, Spire.Doc.FileFormat.PDF);
                }

                using (PrintDialog pd = new())
                {
                    if (pd.ShowDialog() == DialogResult.OK)
                    {
                        using (var document = PdfiumViewer.PdfDocument.Load(pdfLocation))
                        {
                            using (var printDoc = document.CreatePrintDocument())
                            {
                                printDoc.PrinterSettings = new PrinterSettings()
                                {
                                    PrinterName = pd.PrinterSettings.PrinterName
                                };
                                printDoc.Print();
                            }
                        }
                    }
                }
            }
        }

        private void buttonApagar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tem Certeza?", "Confirmacao", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Program.moldura1 = [false, false, false, false, false, false, false, false, false, false];
                Program.moldura2 = [false, false, false, false, false, false, false, false, false, false];
                Program.moldura3 = [false, false, false, false, false, false, false, false, false, false];
                Program.vidro = [false, false, false, false, false, false, false, false, false, false];
                Program.vidro2o = [false, false, false, false, false, false, false, false, false, false];
                Program.vidroAR = [false, false, false, false, false, false, false, false, false, false];
                Program.vidroAR2o = [false, false, false, false, false, false, false, false, false, false];
                Program.espelho = [false, false, false, false, false, false, false, false, false, false];
                Program.mdf = [false, false, false, false, false, false, false, false, false, false];
                Program.paspatur = [false, false, false, false, false, false, false, false, false, false];
                Program.extra = [false, false, false, false, false, false, false, false, false, false];
                Program.quadroExistente = [false, false, false, false, false, false, false, false, false, false];
                Program.moldura1Tipo1 = ["0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000"];
                Program.moldura1Tipo2 = ["0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000"];
                Program.moldura2Tipo1 = ["0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000"];
                Program.moldura2Tipo2 = ["0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000"];
                Program.moldura3Tipo1 = ["0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000"];
                Program.moldura3Tipo2 = ["0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000", "0000"];
                Program.molduraTamanho1 = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
                Program.molduraTamanho2 = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
                Program.molduraExtra = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
                Program.quantidade = [1, 1, 1, 1, 1, 1, 1, 1, 1, 1];
                Program.paspaturTamanho = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
                Program.paspaturValor = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
                Program.extraValor = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
                Program.descricao = ["", "", "", "", "", "", "", "", "", ""];
                Program.valorUnitario = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
                Program.valorTotal = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
                Program.valorTotalGeral = 0;

                textBoxNome.Clear();
                textBoxTelefone.Clear();
                textBoxEndereco.Clear();
                textBoxUsuario.Clear();
                numericUpDownDesconto.Value = 0;
                numericUpDownSinal.Value = 0;
                labelValorTotal.Text = "0";

                this.panelFormLoader.Controls.Clear();
                FormBase formBase = new()
                {
                    Dock = DockStyle.Fill,
                    TopLevel = false,
                    TopMost = true,
                    FormBorderStyle = FormBorderStyle.None
                };
                this.panelFormLoader.Controls.Add(formBase);
                formBase.Show();

                buttonQuadro1.BackColor = Color.FromArgb(24, 30, 54);
                buttonQuadro2.BackColor = Color.FromArgb(24, 30, 54);
                buttonQuadro3.BackColor = Color.FromArgb(24, 30, 54);
                buttonQuadro4.BackColor = Color.FromArgb(24, 30, 54);
                buttonQuadro5.BackColor = Color.FromArgb(24, 30, 54);
                buttonQuadro6.BackColor = Color.FromArgb(24, 30, 54);
                buttonQuadro7.BackColor = Color.FromArgb(24, 30, 54);
                buttonQuadro8.BackColor = Color.FromArgb(24, 30, 54);
                buttonQuadro9.BackColor = Color.FromArgb(24, 30, 54);
                buttonQuadro10.BackColor = Color.FromArgb(24, 30, 54);

                foreach (FormQuadro formQuadro in formQuadros)
                {
                    formQuadro.checkBoxMoldura1.Checked = false;
                    formQuadro.checkBoxMoldura2.Checked = false;
                    formQuadro.checkBoxMoldura3.Checked = false;
                    formQuadro.checkBoxVidro.Checked = false;
                    formQuadro.checkBoxVidro2o.Checked = false;
                    formQuadro.checkBoxVidroAR.Checked = false;
                    formQuadro.checkBoxVidroAR2o.Checked = false;
                    formQuadro.checkBoxEspelho.Checked = false;
                    formQuadro.checkBoxMdf.Checked = false;
                    formQuadro.checkBoxPaspatur.Checked = false;
                    formQuadro.checkBoxMolduraExtra.Checked = false;
                    formQuadro.textBoxMolduraTipo1.Text = "0000";
                    formQuadro.textBoxMolduraTipo1_2.Text = "0000";
                    formQuadro.textBoxMolduraTipo2.Text = "0000";
                    formQuadro.textBoxMolduraTipo2_2.Text = "0000";
                    formQuadro.textBoxMolduraTipo3.Text = "0000";
                    formQuadro.textBoxMolduraTipo3_2.Text = "0000";
                    formQuadro.numericUpDownMoldura1.Value = 0;
                    formQuadro.numericUpDownMoldura1_2.Value = 0;
                    formQuadro.numericUpDownMoldura1_3.Value = 0;
                    formQuadro.numericUpDownQuantidade.Value = 1;
                    formQuadro.numericUpDownPaspaturTamanho.Value = 0;
                    formQuadro.numericUpDownPaspaturValor.Value = 0;
                    formQuadro.numericUpDownExtra.Value = 0;
                    formQuadro.textBoxDescricao.Clear();
                }

            }
        }

        private double excel(int aux, int sheet) //using epplus
        {
            double valor;
            string perfil = formQuadros[aux].textBoxMolduraTipo1.Text;
            string acabamento = formQuadros[aux].textBoxMolduraTipo1_2.Text;
            string path = @"documentos\Tabela Sul America.xlsx";
            var newFile = new FileInfo(path);
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            using (ExcelPackage xlPackage = new(newFile))
            {
                switch (sheet)
                {
                    case 1:
                        ExcelWorksheet ws1 = xlPackage.Workbook.Worksheets["Sheet1"];
                        for (int i = 2; i < 651; i++)
                        {
                            if (ws1.Cells[i, 1].Value != null && ws1.Cells[i, 1].Value.Equals(perfil.ToUpper()))
                            {
                                if (ws1.Cells[i, 2].Value.Equals(acabamento.ToUpper()))
                                {
                                    valor = Convert.ToDouble(ws1.Cells[i, 9].Value);
                                    valor = Convert.ToDouble(valor.ToString("0.00"));
                                    xlPackage.Save();
                                    return valor;
                                }
                            }
                        }
                        break;

                    case 2:
                        ExcelWorksheet ws2 = xlPackage.Workbook.Worksheets["Sheet2"];
                        for (int i = 2; i < 1010; i++)
                        {
                            if (ws2.Cells[i, 1].Value != null && ws2.Cells[i, 1].Value.Equals(perfil.ToUpper()))
                            {
                                valor = Convert.ToDouble(ws2.Cells[i, 9].Value);
                                valor = Convert.ToDouble(valor.ToString("0.00"));
                                xlPackage.Save();
                                return valor;
                            }
                        }
                        break;

                    case 3:
                        ExcelWorksheet ws3 = xlPackage.Workbook.Worksheets["Sheet3"];
                        Dictionary<int, string> tipos = new Dictionary<int, string>();
                        tipos.Add(1, "VIDRO");
                        tipos.Add(2, "VIDROAR");
                        tipos.Add(3, "MDF");
                        tipos.Add(4, "ESPELHO");
                        for (int i = 1; i < 5; i++)
                        {
                            if (ws3.Cells[i, 1].Value != null && ws3.Cells[i, 1].Value.Equals(tipos[aux]))
                            {
                                valor = Convert.ToDouble(ws3.Cells[i, 2].Value);
                                valor = Convert.ToDouble(valor.ToString("0.00"));
                                xlPackage.Save();
                                return valor;
                            }
                        }
                        break;
                }
                return 0;
            }
        }

        private void word(int id, int rowCount) //using DocX
        {
            var doc = DocX.Load(@"documentos\papelTimbradoVidracariaPopular.docx");
            CultureInfo ci = new("pt-BR");
            Xceed.Document.NET.Font font = new Xceed.Document.NET.Font("Arial");
            doc.SetDefaultFont(font);

            doc.ReplaceText(new StringReplaceTextOptions()
            {
                SearchValue = "#usuario",
                NewValue = textBoxUsuario.Text
            });

            doc.ReplaceText(new StringReplaceTextOptions()
            {
                SearchValue = "#id",
                NewValue = id.ToString()
            });

            doc.ReplaceText(new StringReplaceTextOptions()
            {
                SearchValue = "#data",
                NewValue = DateTime.Now.ToString("D", ci)
            });

            doc.ReplaceText(new StringReplaceTextOptions()
            {
                SearchValue = "#nome",
                NewValue = textBoxNome.Text
            });

            doc.ReplaceText(new StringReplaceTextOptions()
            {
                SearchValue = "#telefone",
                NewValue = textBoxTelefone.Text
            });

            doc.ReplaceText(new StringReplaceTextOptions()
            {
                SearchValue = "#endereco",
                NewValue = textBoxEndereco.Text
            });

            if (rowCount < 9)
                rowCount = 8;

            Xceed.Document.NET.Table table = doc.AddTable(rowCount + 1, 9);
            table.Alignment = Alignment.center;
            table.Design = TableDesign.TableGrid;
            table.Rows[0].Cells[0].Paragraphs.First().Append("Qtd").FontSize(9).Bold().Alignment = Alignment.center;
            table.Rows[0].Cells[1].Paragraphs.First().Append("Descri��o(�es)").FontSize(9).Bold().Alignment = Alignment.center; ;
            table.Rows[0].Cells[2].Paragraphs.First().Append("Moldura(s)").FontSize(9).Bold().Alignment = Alignment.center; ;
            table.Rows[0].Cells[3].Paragraphs.First().Append("Vidro").FontSize(9).Bold().Alignment = Alignment.center; ;
            table.Rows[0].Cells[4].Paragraphs.First().Append("Mdf").FontSize(9).Bold().Alignment = Alignment.center; ;
            table.Rows[0].Cells[5].Paragraphs.First().Append("Paspatur").FontSize(9).Bold().Alignment = Alignment.center; ;
            table.Rows[0].Cells[6].Paragraphs.First().Append("Medida(s)").FontSize(9).Bold().Alignment = Alignment.center; ;
            table.Rows[0].Cells[7].Paragraphs.First().Append("Valor Unit�rio").FontSize(9).Bold().Alignment = Alignment.center; ;
            table.Rows[0].Cells[8].Paragraphs.First().Append("Valor Total").FontSize(9).Bold().Alignment = Alignment.center; ;

            int auxProgram = 0;
            int auxTable = 1;
            foreach (var qE in Program.quadroExistente)
            {
                if (qE)
                {
                    table.Rows[auxTable].Cells[0].Paragraphs.First().Append(Program.quantidade[auxProgram].ToString()).FontSize(9);
                    table.Rows[auxTable].Cells[1].Paragraphs.First().Append(Program.descricao[auxProgram]).FontSize(9);

                    if (Program.moldura1Tipo2[auxProgram] == "0000")
                        Program.moldura1Tipo2[auxProgram] = "";
                    if (Program.moldura2Tipo2[auxProgram] == "0000")
                        Program.moldura2Tipo2[auxProgram] = "";
                    if (Program.moldura3Tipo2[auxProgram] == "0000")
                        Program.moldura3Tipo2[auxProgram] = "";


                    if (Program.moldura1[auxProgram] & Program.moldura2[auxProgram] & Program.moldura3[auxProgram])
                        table.Rows[auxTable].Cells[2].Paragraphs.First().Append($"{Program.moldura1Tipo1[auxProgram]} {Program.moldura1Tipo2[auxProgram]}" +
                            $" - {Program.moldura2Tipo1[auxProgram]} {Program.moldura2Tipo2[auxProgram]} - " +
                            $"{Program.moldura3Tipo1[auxProgram]} {Program.moldura3Tipo2[auxProgram]}").FontSize(9);
                    else if (Program.moldura1[auxProgram] & Program.moldura2[auxProgram])
                        table.Rows[auxTable].Cells[2].Paragraphs.First().Append($"{Program.moldura1Tipo1[auxProgram]} {Program.moldura1Tipo2[auxProgram]}" +
                            $" - {Program.moldura2Tipo1[auxProgram]} {Program.moldura2Tipo2[auxProgram]}").FontSize(9);
                    else if (Program.moldura1[auxProgram])
                        table.Rows[auxTable].Cells[2].Paragraphs.First().Append($"{Program.moldura1Tipo1[auxProgram]} {Program.moldura1Tipo2[auxProgram]}").FontSize(9);
                    else
                        table.Rows[auxTable].Cells[2].Paragraphs.First().Append(" ").FontSize(9);

                    if (Program.vidro[auxProgram] & Program.vidroAR[auxProgram])
                        table.Rows[auxTable].Cells[3].Paragraphs.First().Append("2mm-AR").FontSize(9);
                    else if (Program.vidro[auxProgram] & Program.espelho[auxProgram])
                        table.Rows[auxTable].Cells[3].Paragraphs.First().Append("2mm-Espelho").FontSize(9);
                    else if (Program.vidro[auxProgram] & Program.vidro2o[auxProgram])
                        table.Rows[auxTable].Cells[3].Paragraphs.First().Append("2x2mm").FontSize(9);
                    else if (Program.vidroAR[auxProgram] & Program.vidroAR2o[auxProgram])
                        table.Rows[auxTable].Cells[3].Paragraphs.First().Append("2xAR").FontSize(9);
                    else if (Program.vidro[auxProgram])
                        table.Rows[auxTable].Cells[3].Paragraphs.First().Append("2mm").FontSize(9);
                    else if (Program.vidroAR[auxProgram])
                        table.Rows[auxTable].Cells[3].Paragraphs.First().Append("AR").FontSize(9);
                    else if (Program.espelho[auxProgram])
                        table.Rows[auxTable].Cells[3].Paragraphs.First().Append("Espelho").FontSize(9);
                    else
                        table.Rows[auxTable].Cells[3].Paragraphs.First().Append(" ").FontSize(9);

                    if (Program.mdf[auxProgram])
                        table.Rows[auxTable].Cells[4].Paragraphs.First().Append("SIM").FontSize(9);
                    else
                        table.Rows[auxTable].Cells[4].Paragraphs.First().Append(" ").FontSize(9);

                    if (Program.paspatur[auxProgram])
                        table.Rows[auxTable].Cells[5].Paragraphs.First().Append($"{Program.paspaturTamanho[auxProgram]}cm").FontSize(9);
                    else
                        table.Rows[auxTable].Cells[5].Paragraphs.First().Append(" ").FontSize(9);

                    if (Program.molduraTamanho1[auxProgram] != 0 & Program.molduraTamanho2[auxProgram] != 0)
                        table.Rows[auxTable].Cells[6].Paragraphs.First().Append($"{Program.molduraTamanho1[auxProgram]}x{Program.molduraTamanho2[auxProgram]}").FontSize(9);
                    else
                        table.Rows[auxTable].Cells[6].Paragraphs.First().Append(" ").FontSize(9);

                    table.Rows[auxTable].Cells[7].Paragraphs.First().Append(Program.valorUnitario[auxProgram].ToString("0.00")).FontSize(9);
                    table.Rows[auxTable].Cells[8].Paragraphs.First().Append(Program.valorTotal[auxProgram].ToString("0.00")).FontSize(9);

                    auxTable++;
                }
                else
                {
                    table.Rows[auxTable].Cells[0].Paragraphs.First().Append(" ").FontSize(9);
                    table.Rows[auxTable].Cells[1].Paragraphs.First().Append(" ").FontSize(9);
                    table.Rows[auxTable].Cells[2].Paragraphs.First().Append(" ").FontSize(9);
                    table.Rows[auxTable].Cells[3].Paragraphs.First().Append(" ").FontSize(9);
                    table.Rows[auxTable].Cells[4].Paragraphs.First().Append(" ").FontSize(9);
                    table.Rows[auxTable].Cells[5].Paragraphs.First().Append(" ").FontSize(9);
                    table.Rows[auxTable].Cells[6].Paragraphs.First().Append(" ").FontSize(9);
                    table.Rows[auxTable].Cells[7].Paragraphs.First().Append(" ").FontSize(9);
                    table.Rows[auxTable].Cells[8].Paragraphs.First().Append(" ").FontSize(9);

                    if (auxTable < rowCount)
                        auxTable++;
                }
                auxProgram++;
            }
            table.Rows[0].Cells[0].FillColor = Color.FromArgb(175, 171, 171);
            table.Rows[0].Cells[1].FillColor = Color.FromArgb(175, 171, 171);
            table.Rows[0].Cells[2].FillColor = Color.FromArgb(175, 171, 171);
            table.Rows[0].Cells[3].FillColor = Color.FromArgb(175, 171, 171);
            table.Rows[0].Cells[4].FillColor = Color.FromArgb(175, 171, 171);
            table.Rows[0].Cells[5].FillColor = Color.FromArgb(175, 171, 171);
            table.Rows[0].Cells[6].FillColor = Color.FromArgb(175, 171, 171);
            table.Rows[0].Cells[7].FillColor = Color.FromArgb(175, 171, 171);
            table.Rows[0].Cells[8].FillColor = Color.FromArgb(175, 171, 171);

            foreach (var tRow in table.Rows)
            {
                tRow.Cells[0].Width = 10;
                tRow.Cells[1].Width = 200;
                tRow.Cells[2].Width = 250;
                tRow.Cells[3].Width = 55;
                tRow.Cells[4].Width = 35;
                tRow.Cells[5].Width = 55;
                tRow.Cells[6].Width = 55;
                tRow.Cells[7].Width = 60;
                tRow.Cells[8].Width = 60;
            }

            foreach (var row in table.Rows)
            {
                foreach (var cell in row.Cells)
                {
                    cell.VerticalAlignment = VerticalAlignment.Center;
                    foreach (var paragraph in cell.Paragraphs)
                    {
                        paragraph.Font(font);
                        paragraph.Alignment = Alignment.center;
                    }
                }
            }



            doc.ReplaceTextWithObject(new ObjectReplaceTextOptions()
            {
                SearchValue = "#tabela",
                NewObject = table,
                RegExOptions = RegexOptions.IgnoreCase
            });

            doc.ReplaceText(new StringReplaceTextOptions()
            {
                SearchValue = "#valor",
                NewValue = Program.valorTotalGeral.ToString("0.00")
            });

            doc.ReplaceText(new StringReplaceTextOptions()
            {
                SearchValue = "#desconto",
                NewValue = numericUpDownDesconto.Value.ToString("0.00")
            });

            doc.ReplaceText(new StringReplaceTextOptions()
            {
                SearchValue = "#sinal",
                NewValue = numericUpDownSinal.Value.ToString("0.00")
            });

            doc.ReplaceText(new StringReplaceTextOptions()
            {
                SearchValue = "#vTotal",
                NewValue = ((decimal)Program.valorTotalGeral - numericUpDownDesconto.Value - numericUpDownSinal.Value).ToString("0.00")
            });

            doc.SaveAs(@"documentos\papelTimbradoGerado.docx");
        }

        private void buttonQuadro1_Click(object sender, EventArgs e)
        {
            this.panelFormLoader.Controls.Clear();
            this.panelFormLoader.Controls.Add(formQuadros[0]);
            formQuadros[0].Show();

            buttonQuadro2.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro3.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro4.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro5.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro6.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro7.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro8.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro9.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro10.BackColor = Color.FromArgb(24, 30, 54);

            buttonQuadro1.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void buttonQuadro2_Click(object sender, EventArgs e)
        {
            this.panelFormLoader.Controls.Clear();
            this.panelFormLoader.Controls.Add(formQuadros[1]);
            formQuadros[1].Show();

            buttonQuadro1.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro3.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro4.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro5.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro6.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro7.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro8.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro9.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro10.BackColor = Color.FromArgb(24, 30, 54);

            buttonQuadro2.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void buttonQuadro3_Click(object sender, EventArgs e)
        {
            this.panelFormLoader.Controls.Clear();
            this.panelFormLoader.Controls.Add(formQuadros[2]);
            formQuadros[2].Show();

            buttonQuadro1.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro2.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro4.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro5.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro6.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro7.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro8.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro9.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro10.BackColor = Color.FromArgb(24, 30, 54);

            buttonQuadro3.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void buttonQuadro4_Click(object sender, EventArgs e)
        {
            this.panelFormLoader.Controls.Clear();
            this.panelFormLoader.Controls.Add(formQuadros[3]);
            formQuadros[3].Show();

            buttonQuadro1.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro2.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro3.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro5.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro6.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro7.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro8.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro9.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro10.BackColor = Color.FromArgb(24, 30, 54);

            buttonQuadro4.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void buttonQuadro5_Click(object sender, EventArgs e)
        {
            this.panelFormLoader.Controls.Clear();
            this.panelFormLoader.Controls.Add(formQuadros[4]);
            formQuadros[4].Show();

            buttonQuadro1.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro2.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro3.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro4.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro6.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro7.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro8.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro9.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro10.BackColor = Color.FromArgb(24, 30, 54);

            buttonQuadro5.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void buttonQuadro6_Click(object sender, EventArgs e)
        {
            this.panelFormLoader.Controls.Clear();
            this.panelFormLoader.Controls.Add(formQuadros[5]);
            formQuadros[5].Show();

            buttonQuadro1.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro2.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro3.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro4.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro5.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro7.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro8.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro9.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro10.BackColor = Color.FromArgb(24, 30, 54);

            buttonQuadro6.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void buttonQuadro7_Click(object sender, EventArgs e)
        {
            this.panelFormLoader.Controls.Clear();
            this.panelFormLoader.Controls.Add(formQuadros[6]);
            formQuadros[6].Show();

            buttonQuadro1.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro2.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro3.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro4.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro5.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro6.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro8.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro9.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro10.BackColor = Color.FromArgb(24, 30, 54);

            buttonQuadro7.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void buttonQuadro8_Click(object sender, EventArgs e)
        {
            this.panelFormLoader.Controls.Clear();
            this.panelFormLoader.Controls.Add(formQuadros[7]);
            formQuadros[7].Show();

            buttonQuadro1.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro2.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro3.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro4.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro5.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro6.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro7.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro9.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro10.BackColor = Color.FromArgb(24, 30, 54);

            buttonQuadro8.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void buttonQuadro9_Click(object sender, EventArgs e)
        {
            this.panelFormLoader.Controls.Clear();
            this.panelFormLoader.Controls.Add(formQuadros[8]);
            formQuadros[8].Show();

            buttonQuadro1.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro2.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro3.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro4.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro5.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro6.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro7.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro8.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro10.BackColor = Color.FromArgb(24, 30, 54);

            buttonQuadro9.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void buttonQuadro10_Click(object sender, EventArgs e)
        {
            this.panelFormLoader.Controls.Clear();
            this.panelFormLoader.Controls.Add(formQuadros[9]);
            formQuadros[9].Show();

            buttonQuadro1.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro2.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro3.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro4.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro5.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro6.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro7.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro8.BackColor = Color.FromArgb(24, 30, 54);
            buttonQuadro9.BackColor = Color.FromArgb(24, 30, 54);

            buttonQuadro10.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void buttonSelecionar_Click(object sender, EventArgs e)
        {
            Button[] buttonArray = [ buttonQuadro1, buttonQuadro2, buttonQuadro3, buttonQuadro4, buttonQuadro5, buttonQuadro6,
                buttonQuadro7, buttonQuadro8, buttonQuadro9, buttonQuadro10 ];

            bool cbMoldura1 = false;
            bool cbMoldura2 = false;
            bool cbMoldura3 = false;
            bool cbVidro = false;
            bool cbVidro2o = false;
            bool cbVidroAR = false;
            bool cbVidroAR2o = false;
            bool cbEspelho = false;
            bool cbMdf = false;
            bool cbPaspatur = false;
            bool cbMolduraExtra = false;
            string nome = "";
            string telefone = "";
            string endereco = "";
            string usuario = "";
            string sMolduraTipo1 = "0000";
            string sMolduraTipo1_2 = "0000";
            string sMolduraTipo2 = "0000";
            string sMolduraTipo2_2 = "0000";
            string sMolduraTipo3 = "0000";
            string sMolduraTipo3_2 = "0000";
            string sDescricao = "";
            decimal nupMoldura1 = 0;
            decimal nupMoldura2 = 0;
            decimal nupMoldura3 = 0;
            decimal nupQuantidade = 1;
            decimal nupPaspaturQnt = 0;
            decimal nupPaspaturVlr = 0;
            decimal nupExtra = 0;
            decimal desconto = 0;
            decimal sinal = 0;

            for (int i = 0; i < 10; i++)
            {
                if (buttonArray[i].BackColor.Equals(Color.FromArgb(46, 51, 73)))
                {
                    nome = textBoxNome.Text;
                    telefone = textBoxTelefone.Text;
                    endereco = textBoxEndereco.Text;
                    usuario = textBoxUsuario.Text;
                    desconto = numericUpDownDesconto.Value;
                    sinal = numericUpDownSinal.Value;

                    cbMoldura1 = formQuadros[i].checkBoxMoldura1.Checked;
                    cbMoldura2 = formQuadros[i].checkBoxMoldura2.Checked;
                    cbMoldura3 = formQuadros[i].checkBoxMoldura3.Checked;
                    cbVidro = formQuadros[i].checkBoxVidro.Checked;
                    cbVidro2o = formQuadros[i].checkBoxVidro2o.Checked;
                    cbVidroAR = formQuadros[i].checkBoxVidroAR.Checked;
                    cbVidroAR2o = formQuadros[i].checkBoxVidroAR2o.Checked;
                    cbEspelho = formQuadros[i].checkBoxEspelho.Checked;
                    cbMdf = formQuadros[i].checkBoxMdf.Checked;
                    cbPaspatur = formQuadros[i].checkBoxPaspatur.Checked;
                    cbMolduraExtra = formQuadros[i].checkBoxMolduraExtra.Checked;
                    sMolduraTipo1 = formQuadros[i].textBoxMolduraTipo1.Text;
                    sMolduraTipo1_2 = formQuadros[i].textBoxMolduraTipo1_2.Text;
                    sMolduraTipo2 = formQuadros[i].textBoxMolduraTipo2.Text;
                    sMolduraTipo2_2 = formQuadros[i].textBoxMolduraTipo2_2.Text;
                    sMolduraTipo3 = formQuadros[i].textBoxMolduraTipo3.Text;
                    sMolduraTipo3_2 = formQuadros[i].textBoxMolduraTipo3_2.Text;
                    sDescricao = formQuadros[i].textBoxDescricao.Text;
                    nupMoldura1 = formQuadros[i].numericUpDownMoldura1.Value;
                    nupMoldura2 = formQuadros[i].numericUpDownMoldura1_2.Value;
                    nupMoldura3 = formQuadros[i].numericUpDownMoldura1_3.Value;
                    nupQuantidade = formQuadros[i].numericUpDownQuantidade.Value;
                    nupPaspaturQnt = formQuadros[i].numericUpDownPaspaturTamanho.Value;
                    nupPaspaturVlr = formQuadros[i].numericUpDownPaspaturValor.Value;
                    nupExtra = formQuadros[i].numericUpDownExtra.Value;
                }
            }
            buttonApagar.PerformClick();

            textBoxNome.Text = nome;
            textBoxTelefone.Text = telefone;
            textBoxEndereco.Text = endereco;
            textBoxUsuario.Text = usuario;
            numericUpDownDesconto.Value = desconto;
            numericUpDownSinal.Value = sinal;

            formQuadros[0].checkBoxMoldura1.Checked = cbMoldura1;
            formQuadros[0].checkBoxMoldura2.Checked = cbMoldura2;
            formQuadros[0].checkBoxMoldura3.Checked = cbMoldura3;
            formQuadros[0].checkBoxVidro.Checked = cbVidro;
            formQuadros[0].checkBoxVidro2o.Checked = cbVidro2o;
            formQuadros[0].checkBoxVidroAR.Checked = cbVidroAR;
            formQuadros[0].checkBoxVidroAR2o.Checked = cbVidroAR2o;
            formQuadros[0].checkBoxEspelho.Checked = cbEspelho;
            formQuadros[0].checkBoxMdf.Checked = cbMdf;
            formQuadros[0].checkBoxPaspatur.Checked = cbPaspatur;
            formQuadros[0].checkBoxMolduraExtra.Checked = cbMolduraExtra;
            formQuadros[0].textBoxMolduraTipo1.Text = sMolduraTipo1;
            formQuadros[0].textBoxMolduraTipo1_2.Text = sMolduraTipo1_2;
            formQuadros[0].textBoxMolduraTipo2.Text = sMolduraTipo2;
            formQuadros[0].textBoxMolduraTipo2_2.Text = sMolduraTipo2_2;
            formQuadros[0].textBoxMolduraTipo3.Text = sMolduraTipo3;
            formQuadros[0].textBoxMolduraTipo3_2.Text = sMolduraTipo3_2;
            formQuadros[0].textBoxDescricao.Text = sDescricao;
            formQuadros[0].numericUpDownMoldura1.Value = nupMoldura1;
            formQuadros[0].numericUpDownMoldura1_2.Value = nupMoldura2;
            formQuadros[0].numericUpDownMoldura1_3.Value = nupMoldura3;
            formQuadros[0].numericUpDownQuantidade.Value = nupQuantidade;
            formQuadros[0].numericUpDownPaspaturTamanho.Value = nupPaspaturQnt;
            formQuadros[0].numericUpDownPaspaturValor.Value = nupPaspaturVlr;
            formQuadros[0].numericUpDownExtra.Value = nupExtra;
            buttonQuadro1.PerformClick();
        }

        private void buttonAdicionar_Click(object sender, EventArgs e)
        {
            if (textBoxNome.Text == "")
            {
                MessageBox.Show("N�o Deixe Valores Zerados", "Valor Zerado Nome",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (textBoxTelefone.Text == "")
            {
                MessageBox.Show("N�o Deixe Valores Zerados", "Valor Zerado Telefone",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (textBoxEndereco.Text == "")
            {
                MessageBox.Show("N�o Deixe Valores Zerados", "Valor Zerado Endere�o",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (MessageBox.Show("Tem Certeza que quer adicionar o cliente?", "Confirmacao Adicionar Cliente", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DALClientes.Adicionar(textBoxNome.Text, textBoxEndereco.Text, textBoxTelefone.Text);

                DALClientes.CriarLista();
                atualizarListaClientes();
            }
        }

        private void buttonDeletar_Click(object sender, EventArgs e)
        {
            if (listBoxListaClientes.SelectedItem != null)
            {
                if (MessageBox.Show("Tem Certeza que quer deletar o cliente?", "Confirmacao Deletar Cliente", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    var cliente = (Cliente)listBoxListaClientes.SelectedItem;
                    DALClientes.Deletar(cliente.Id);

                    DALClientes.CriarLista();
                    atualizarListaClientes();
                }
            }
            else
            {
                MessageBox.Show("Primeiro selecione o cliente na lista", "Deletar Cliente",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void listBoxListaClientes_DoubleClick(object sender, EventArgs e)
        {

            if (listBoxListaClientes.SelectedItem != null)
            {
                var cliente = (Cliente)listBoxListaClientes.SelectedItem;
                textBoxNome.Text = cliente.Nome;
                textBoxEndereco.Text = cliente.Endereco;
                textBoxTelefone.Text = cliente.Telefone;
            }
        }

        private void atualizarListaClientes()
        {
            listBoxListaClientes.Items.Clear();

            foreach (var cliente in DALClientes.listaClientes)
            {
                listBoxListaClientes.Items.Add(cliente);
            }
            listBoxListaClientes.Refresh();
        }

        private void textBoxProcura_TextChanged(object sender, EventArgs e)
        {
            DALClientes.Procurar(textBoxProcura.Text);
            atualizarListaClientes();
        }
    }
}
