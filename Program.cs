private void ClickButton(object sender, EventArgs e)
{
    if (this.passportTextbox.Text.Trim() == "")
    {
        int passportData = (int)MessageBox.Show("Введите серию и номер паспорта");
    }
    else
    {
        string rawData = this.passportTextbox.Text.Trim().Replace(" ", string.Empty);

        int minLenghtRawData = 10;

        if (rawData.Length < minLenghtRawData)
        {
            this.textResult.Text = "Неверный формат серии или номера паспорта";
        }
        else
        {
            string commandText = string.Format("select * from passports where num='{0}' limit 1;", (object)Form1.ComputeSha256Hash(rawData));
            string connectionString = string.Format("Data Source=" + Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\db.sqlite");

            try
            {
                SQLiteConnection connection = new SQLiteConnection(connectionString);
                connection.Open();
                SQLiteDataAdapter sqLiteDataAdapter = new SQLiteDataAdapter(new SQLiteCommand(commandText, connection));
                DataTable dataTable1 = new DataTable();
                DataTable dataTable2 = dataTable1;
                sqLiteDataAdapter.Fill(dataTable2);

                if (dataTable1.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dataTable1.Rows[0].ItemArray[1]))
                        this.textResult.Text = "По паспорту «" + this.passportTextbox.Text + "» доступ к бюллетеню на дистанционном электронном голосовании ПРЕДОСТАВЛЕН";
                    else
                        this.textResult.Text = "По паспорту «" + this.passportTextbox.Text + "» доступ к бюллетеню на дистанционном электронном голосовании НЕ ПРЕДОСТАВЛЯЛСЯ";
                }
                else
                {
                    this.textResult.Text = "Паспорт «" + this.passportTextbox.Text + "» в списке участников дистанционного голосования НЕ НАЙДЕН";
                }

                connection.Close();
            }
            catch (SQLiteException exception)
            {
                if (exception.ErrorCode != 1)
                    return;

                int fileNotFound = (int)MessageBox.Show("Файл db.sqlite не найден. Положите файл в папку вместе с exe.");
            }
        }
    }
}