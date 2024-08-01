﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppFuncionario
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
            Funcionario funcionario = new Funcionario();
            string data = DateTime.Now.ToString("dd/MM/yyyy");
            if (funcionario.Aniversario(data) == false)
            {
                pbxAniversario.Visible = false;
            }


        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Environment.Exit(0); //Faz encerrar o processo do programa
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            Funcionario funcionario = new Funcionario();
            List<Funcionario> func = funcionario.Listafuncionario();
            dgvFuncionario.DataSource = func;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            this.ActiveControl = txtNome; //cursor no campo nome
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            if (txtNome.Text == "" || txtMatricula.Text == "") {
                MessageBox.Show("Por favor, preencha todos os campos!");
                return;
            }
            try
            {
                Funcionario funcionario = new Funcionario();
                if (funcionario.RegistroRepetido(txtMatricula.Text) == true)
                {
                    MessageBox.Show("Funcionário já existe em nossa base de dados!");
                    txtNome.Text = string.Empty;
                    txtMatricula.Text = string.Empty;
                    txtDataNascimento.Text = string.Empty;
                    cbxTurno.Text = string.Empty;
                    this.ActiveControl = txtNome;
                }
                else
                {
                    string turno = cbxTurno.SelectedItem.ToString();
                    funcionario.Inserir(txtNome.Text, turno, txtDataNascimento.Text, txtMatricula.Text);
                    MessageBox.Show("Funcionário cadastrado com sucesso!");
                    List<Funcionario> func = funcionario.Listafuncionario();
                    dgvFuncionario.DataSource = func;
                    txtNome.Text = string.Empty;
                    txtMatricula.Text = string.Empty;
                    txtDataNascimento.Text = string.Empty;
                    cbxTurno.Text = string.Empty;
                    string data = DateTime.Now.ToString("dd/MM/yyyy");
                    if(funcionario.Aniversario(data) == false)
                    {
                        pbxAniversario.Visible = false;
                    }
                    else
                    {
                        pbxAniversario.Visible=true;
                    }

                }
            }
            catch (Exception erro)
            {

                MessageBox.Show(erro.Message);
            }
            

        }
            
    }
}
