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

        public void checkBoxMoldura1_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxVidro.Checked & !checkBoxMdf.Checked & !checkBoxVidroAR.Checked & !checkBoxEspelho.Checked)
            {
                labelMoldura1Tipo.Visible = checkBoxMoldura1.Checked;
                numericUpDownMoldura1.Visible = checkBoxMoldura1.Checked;
                numericUpDownMoldura1_2.Visible = checkBoxMoldura1.Checked;
            }
            if (!checkBoxMolduraExtra.Checked & !checkBoxVidro.Checked & !checkBoxMdf.Checked & !checkBoxPaspatur.Checked & !checkBoxVidroAR.Checked & !checkBoxEspelho.Checked)
            {
                labelDescricao1.Visible = checkBoxMoldura1.Checked;
                textBoxDescricao.Visible = checkBoxMoldura1.Checked;
                labelQuantidade1.Visible = checkBoxMoldura1.Checked;
                numericUpDownQuantidade.Visible = checkBoxMoldura1.Checked;
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
            checkBoxVidro2o.Visible = checkBoxVidro.Checked;
            if (!checkBoxMoldura1.Checked & !checkBoxMdf.Checked & !checkBoxVidroAR.Checked & !checkBoxEspelho.Checked)
            {
                labelMoldura1Tipo.Visible = checkBoxVidro.Checked;
                numericUpDownMoldura1.Visible = checkBoxVidro.Checked;
                numericUpDownMoldura1_2.Visible = checkBoxVidro.Checked;
            }
            if (!checkBoxMoldura1.Checked & !checkBoxMolduraExtra.Checked & !checkBoxMdf.Checked & !checkBoxPaspatur.Checked & !checkBoxVidroAR.Checked & !checkBoxEspelho.Checked)
            {
                labelDescricao1.Visible = checkBoxVidro.Checked;
                textBoxDescricao.Visible = checkBoxVidro.Checked;
                labelQuantidade1.Visible = checkBoxVidro.Checked;
                numericUpDownQuantidade.Visible = checkBoxVidro.Checked;
            }
        }

        private void checkBoxVidro2o_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void checkBoxVidroAR_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxVidroAR2o.Visible = checkBoxVidroAR.Checked;
            if (!checkBoxMoldura1.Checked & !checkBoxMdf.Checked & !checkBoxVidro.Checked & !checkBoxEspelho.Checked)
            {
                labelMoldura1Tipo.Visible = checkBoxVidroAR.Checked;
                numericUpDownMoldura1.Visible = checkBoxVidroAR.Checked;
                numericUpDownMoldura1_2.Visible = checkBoxVidroAR.Checked;
            }
            if (!checkBoxMoldura1.Checked & !checkBoxMolduraExtra.Checked & !checkBoxMdf.Checked & !checkBoxPaspatur.Checked & !checkBoxVidro.Checked & !checkBoxEspelho.Checked)
            {
                labelDescricao1.Visible = checkBoxVidroAR.Checked;
                textBoxDescricao.Visible = checkBoxVidroAR.Checked;
                labelQuantidade1.Visible = checkBoxVidroAR.Checked;
                numericUpDownQuantidade.Visible = checkBoxVidroAR.Checked;
            }
        }

        private void checkBoxVidroAR2o_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxEspelho_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxMoldura1.Checked & !checkBoxMdf.Checked & !checkBoxVidro.Checked & !checkBoxVidroAR.Checked)
            {
                labelMoldura1Tipo.Visible = checkBoxEspelho.Checked;
                numericUpDownMoldura1.Visible = checkBoxEspelho.Checked;
                numericUpDownMoldura1_2.Visible = checkBoxEspelho.Checked;
            }
            if (!checkBoxMoldura1.Checked & !checkBoxMolduraExtra.Checked & !checkBoxMdf.Checked & !checkBoxPaspatur.Checked & !checkBoxVidro.Checked & !checkBoxVidroAR.Checked)
            {
                labelDescricao1.Visible = checkBoxEspelho.Checked;
                textBoxDescricao.Visible = checkBoxEspelho.Checked;
                labelQuantidade1.Visible = checkBoxEspelho.Checked;
                numericUpDownQuantidade.Visible = checkBoxEspelho.Checked;
            }
        }
        private void checkBoxMdf_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxMoldura1.Checked & !checkBoxVidro.Checked & !checkBoxVidroAR.Checked)
            {
                labelMoldura1Tipo.Visible = checkBoxMdf.Checked;
                numericUpDownMoldura1.Visible = checkBoxMdf.Checked;
                numericUpDownMoldura1_2.Visible = checkBoxMdf.Checked;
            }
            if (!checkBoxMoldura1.Checked & !checkBoxMolduraExtra.Checked & !checkBoxVidro.Checked & !checkBoxPaspatur.Checked & !checkBoxVidroAR.Checked)
            {
                labelDescricao1.Visible = checkBoxMdf.Checked;
                textBoxDescricao.Visible = checkBoxMdf.Checked;
                labelQuantidade1.Visible = checkBoxMdf.Checked;
                numericUpDownQuantidade.Visible = checkBoxMdf.Checked;
            }
        }

        private void checkBoxPaspatur_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxMoldura1.Checked & !checkBoxMolduraExtra.Checked & !checkBoxVidro.Checked & !checkBoxMdf.Checked & !checkBoxVidroAR.Checked)
            {
                labelDescricao1.Visible = checkBoxPaspatur.Checked;
                textBoxDescricao.Visible = checkBoxPaspatur.Checked;
                labelQuantidade1.Visible = checkBoxPaspatur.Checked;
                numericUpDownQuantidade.Visible = checkBoxPaspatur.Checked;
            }
            numericUpDownPaspaturTamanho.Visible = checkBoxPaspatur.Checked;
            numericUpDownPaspaturValor.Visible = checkBoxPaspatur.Checked;
            labelPaspaturValor.Visible = checkBoxPaspatur.Checked;
        }

        private void checkBoxMolduraExtra_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxMoldura1.Checked & !checkBoxVidro.Checked & !checkBoxMdf.Checked & !checkBoxPaspatur.Checked & !checkBoxVidroAR.Checked)
            {
                labelDescricao1.Visible = checkBoxMolduraExtra.Checked;
                textBoxDescricao.Visible = checkBoxMolduraExtra.Checked;
            }
            numericUpDownExtra.Visible = checkBoxMolduraExtra.Checked;
        }
    }
}
