﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.IO;
using ClosedXML.Excel;

namespace Proyecto_Base_de_Datos
{
    public partial class ImprimirRecibo : Form
    {
        public ImprimirRecibo()
        {
            InitializeComponent();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

            if(printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Recibo recibo = new Recibo();
            recibo.SeleccionarRecibo(registroRecibo.id);

            e.Graphics.DrawString(recibo.numFolio, new System.Drawing.Font("Arial", 16, FontStyle.Bold), Brushes.Black, 150, 125);
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            Recibo recibo = new Recibo();
            recibo.SeleccionarRecibo(registroRecibo.id);

            SaveFileDialog guardar = new SaveFileDialog();
            guardar.FileName = recibo.numFolio.ToString() + ".pdf";
            guardar.ShowDialog();

            string paginahtml_texto = Properties.Resources.paginahtmlrecibos.ToString();

            paginahtml_texto = paginahtml_texto.Replace("@NUMFOLIO", recibo.numFolio);
            paginahtml_texto = paginahtml_texto.Replace("@SOCIO", recibo.reciboSocio);

            paginahtml_texto = paginahtml_texto.Replace("@FECHA", recibo.fecha);

            paginahtml_texto = paginahtml_texto.Replace("@IMPORTE", "$"+recibo.importe);

            paginahtml_texto = paginahtml_texto.Replace("@IMPORTELETRA", recibo.importeLetra);

            paginahtml_texto = paginahtml_texto.Replace("@PERIODO", recibo.periodo);

            if (guardar.ShowDialog() == DialogResult.OK)
            {
                using(FileStream stream = new FileStream(guardar.FileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);

                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);

                    pdfDoc.Open();

                    pdfDoc.Add(new Phrase(""));

                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(Properties.Resources.Logo, System.Drawing.Imaging.ImageFormat.Png);

                    img.ScaleToFit(80, 60);

                    img.Alignment = iTextSharp.text.Image.UNDERLYING;
                    img.SetAbsolutePosition(pdfDoc.LeftMargin, pdfDoc.Top - 60);

                    pdfDoc.Add(img);

                    using (StringReader sr = new StringReader(paginahtml_texto))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    }

                    pdfDoc.Close();

                    stream.Close();
                }

                


            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            using(SaveFileDialog sfd = new SaveFileDialog() { Filter="Excel Workbook|*.xlsx" })
            {
                if(sfd.ShowDialog() == DialogResult.OK)
                {
                    /* try
                    {
                        using(XLWorkbook workbook = new XLWorkbook())
                        {
                            workbook.Worksheets.Add(bDProyectoDataSet1.recibo.CopyToDataTable(), "Recibos");
                            workbook.SaveAs(sfd.FileName);
                        }

                        MessageBox.Show("Se exportó correctamente a Excel", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    } */

                    using (XLWorkbook workbook = new XLWorkbook())
                    {
                        workbook.Worksheets.Add(bDProyectoDataSet1.recibo.CopyToDataTable(), "Recibos");
                        workbook.SaveAs(sfd.FileName);
                    }

                    MessageBox.Show("Se exportó correctamente a Excel", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ImprimirRecibo_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'bDProyectoDataSet1.recibo' Puede moverla o quitarla según sea necesario.
            this.reciboTableAdapter.Fill(this.bDProyectoDataSet1.recibo);

        }

        private void btnCorreo_Click(object sender, EventArgs e)
        {
            Correo correo = new Correo();

            correo.Show();
        }
    }
}
