using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AplicativoVidracariaPopular
{
    public partial class FormQuadro : Form
    {
        public FormQuadro()
        {
            InitializeComponent();
        }

        private void FormQuadro1_Load(object sender, EventArgs e)
        {

        }

        public void checkBoxMoldura1_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxVidro.Checked & !checkBoxMdf.Checked)
            {
                labelMoldura1Tipo.Visible = checkBoxMoldura1.Checked;
                numericUpDownMoldura1.Visible = checkBoxMoldura1.Checked;
                numericUpDownMoldura1_2.Visible = checkBoxMoldura1.Checked;
                labelQuantidade1.Visible = checkBoxMoldura1.Checked;
                numericUpDownQuantidade.Visible = checkBoxMoldura1.Checked;
            }
            if (!checkBoxMolduraExtra.Checked & !checkBoxVidro.Checked & !checkBoxMdf.Checked & !checkBoxPaspatur.Checked)
            {
                labelDescricao1.Visible = checkBoxMoldura1.Checked;
                textBoxDescricao.Visible = checkBoxMoldura1.Checked;
            }
            numericUpDownMoldura1_3.Visible = checkBoxMoldura1.Checked;
            textBoxMolduraTipo1.Visible = checkBoxMoldura1.Checked;
            textBoxMolduraTipo1_2.Visible = checkBoxMoldura1.Checked;
        }

        private void checkBoxMoldura2_CheckedChanged(object sender, EventArgs e)
        {
            textBoxMolduraTipo2.Visible = checkBoxMoldura2.Checked;
            textBoxMolduraTipo2_2.Visible = checkBoxMoldura2.Checked;
        }

        private void checkBoxMoldura3_CheckedChanged(object sender, EventArgs e)
        {
            textBoxMolduraTipo3.Visible = checkBoxMoldura3.Checked;
            textBoxMolduraTipo3_2.Visible = checkBoxMoldura3.Checked;
        }

        private void checkBoxVidro_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxMoldura1.Checked & !checkBoxMdf.Checked)
            {
                labelMoldura1Tipo.Visible = checkBoxVidro.Checked;
                numericUpDownMoldura1.Visible = checkBoxVidro.Checked;
                numericUpDownMoldura1_2.Visible = checkBoxVidro.Checked;
                labelQuantidade1.Visible = checkBoxVidro.Checked;
                numericUpDownQuantidade.Visible = checkBoxVidro.Checked;
            }
            if (!checkBoxMoldura1.Checked & !checkBoxMolduraExtra.Checked & !checkBoxMdf.Checked & !checkBoxPaspatur.Checked)
            {
                labelDescricao1.Visible = checkBoxVidro.Checked;
                textBoxDescricao.Visible = checkBoxVidro.Checked;
            }
        }

        private void checkBoxMdf_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxMoldura1.Checked & !checkBoxVidro.Checked)
            {
                labelMoldura1Tipo.Visible = checkBoxMdf.Checked;
                numericUpDownMoldura1.Visible = checkBoxMdf.Checked;
                numericUpDownMoldura1_2.Visible = checkBoxMdf.Checked;
                labelQuantidade1.Visible = checkBoxMdf.Checked;
                numericUpDownQuantidade.Visible = checkBoxMdf.Checked;
            }
            if (!checkBoxMoldura1.Checked & !checkBoxMolduraExtra.Checked & !checkBoxVidro.Checked & !checkBoxPaspatur.Checked)
            {
                labelDescricao1.Visible = checkBoxMdf.Checked;
                textBoxDescricao.Visible = checkBoxMdf.Checked;
            }
        }

        private void checkBoxPaspatur_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxMoldura1.Checked & !checkBoxMolduraExtra.Checked & !checkBoxVidro.Checked & !checkBoxMdf.Checked)
            {
                labelDescricao1.Visible = checkBoxPaspatur.Checked;
                textBoxDescricao.Visible = checkBoxPaspatur.Checked;
            }
            numericUpDownPaspaturTamanho.Visible = checkBoxPaspatur.Checked;
            numericUpDownPaspaturValor.Visible = checkBoxPaspatur.Checked;
            labelPaspaturValor.Visible = checkBoxPaspatur.Checked;
        }

        private void checkBoxMolduraExtra_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxMoldura1.Checked & !checkBoxVidro.Checked & !checkBoxMdf.Checked & !checkBoxPaspatur.Checked)
            {
                labelDescricao1.Visible = checkBoxMolduraExtra.Checked;
                textBoxDescricao.Visible = checkBoxMolduraExtra.Checked;
            }
            numericUpDownExtra.Visible = checkBoxMolduraExtra.Checked;
        }
    }
}
