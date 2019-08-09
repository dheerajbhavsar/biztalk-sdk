'-----------------------------------------------------------------------
' File      : XMLCreatorForm.vb
'
' Summary   : This application will assist in create a XML document for 
'             testing with MSMQTLarge sample.
'
' Sample    : MSMQTLarge Sample
'
'-----------------------------------------------------------------------
' This file is part of the Microsoft BizTalk Server 2016 SDK
' 
' Copyright (c) Microsoft Corporation. All rights reserved.
'
' This source code is intended only as a supplement to Microsoft BizTalk
' Server 2006 release and/or on-line documentation. See these other
' materials for detailed information regarding Microsoft code samples.
'
' THIS CODE AND INFORMATION ARE PROVIDED "AS-IS" WITHOUT WARRANTY OF ANY
' KIND, WHETHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
' IMPLIED WARRANTIES AND MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
' PURPOSE.
'-----------------------------------------------------------------------

Imports System.IO
Imports System
Imports System.Xml



Public Class Form1
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents XMLBody As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents XMLCopies As System.Windows.Forms.TextBox
    Friend WithEvents RootTop As System.Windows.Forms.TextBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents FilePath As System.Windows.Forms.TextBox
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.RootTop = New System.Windows.Forms.TextBox
        Me.XMLBody = New System.Windows.Forms.TextBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.XMLCopies = New System.Windows.Forms.TextBox
        Me.FilePath = New System.Windows.Forms.TextBox
        Me.Button2 = New System.Windows.Forms.Button
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'RootTop
        '
        Me.RootTop.Location = New System.Drawing.Point(24, 24)
        Me.RootTop.Name = "RootTop"
        Me.RootTop.Size = New System.Drawing.Size(144, 20)
        Me.RootTop.TabIndex = 0
        Me.RootTop.Text = "<ROOT>"
        '
        'XMLBody
        '
        Me.XMLBody.Location = New System.Drawing.Point(24, 80)
        Me.XMLBody.Multiline = True
        Me.XMLBody.Name = "XMLBody"
        Me.XMLBody.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.XMLBody.Size = New System.Drawing.Size(264, 184)
        Me.XMLBody.TabIndex = 1
        Me.XMLBody.Text = ""
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(96, 408)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(152, 24)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "Create XML"
        '
        'XMLCopies
        '
        Me.XMLCopies.Location = New System.Drawing.Point(24, 304)
        Me.XMLCopies.Name = "XMLCopies"
        Me.XMLCopies.Size = New System.Drawing.Size(72, 20)
        Me.XMLCopies.TabIndex = 4
        Me.XMLCopies.Text = "1"
        Me.XMLCopies.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'FilePath
        '
        Me.FilePath.Location = New System.Drawing.Point(24, 360)
        Me.FilePath.Name = "FilePath"
        Me.FilePath.Size = New System.Drawing.Size(208, 20)
        Me.FilePath.TabIndex = 5
        Me.FilePath.Text = ""
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(248, 360)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(24, 23)
        Me.Button2.TabIndex = 6
        Me.Button2.Text = "..."
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(24, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 16)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Root Name"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(24, 64)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(136, 16)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "XML Body"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(24, 280)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(184, 16)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "# of times to copy XML body"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(24, 344)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(144, 16)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "XML File Location"
        '
        'Form1
        '
        Me.ClientSize = New System.Drawing.Size(344, 437)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.FilePath)
        Me.Controls.Add(Me.XMLCopies)
        Me.Controls.Add(Me.XMLBody)
        Me.Controls.Add(Me.RootTop)
        Me.Controls.Add(Me.Button1)
        Me.Name = "Form1"
        Me.Text = "XML Creator"
        Me.ResumeLayout(False)

    End Sub

#End Region
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim intCounter, intXMLBodyCount As Integer
        Dim objStreamWriter As StreamWriter
        Dim strTemp As String
        Dim strFileName As String

        intXMLBodyCount = XMLCopies.Text
        strFileName = FilePath.Text
        objStreamWriter = New StreamWriter(strFileName)
        Dim objCursor As Cursor = Me.Cursor
        Try
            Me.Cursor = Cursors.WaitCursor
            objStreamWriter.WriteLine(RootTop.Text)

            intCounter = 1

            Do While intCounter <= intXMLBodyCount
                objStreamWriter.WriteLine(XMLBody.Text)
                intCounter = intCounter + 1
            Loop

            strTemp = RootTop.Text
            strTemp = strTemp.Replace("<", "</")
            objStreamWriter.WriteLine(strTemp)

        Finally
            Me.Cursor = objCursor
            objStreamWriter.Close()
        End Try

        MessageBox.Show("Successfully created file.", "XML Creator")

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        With SaveFileDialog1
            .OverwritePrompt = True
            .CheckFileExists = False
            .CheckPathExists = True
            .DefaultExt = ".xml"
            .InitialDirectory = "c:\test"
            .Title = "  Select File To Save To..."
        End With

        ' If the user clicked CANCEL, exit out
        If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
            ' Store the file name
            FilePath.Text = SaveFileDialog1.FileName
        End If
    End Sub
End Class
