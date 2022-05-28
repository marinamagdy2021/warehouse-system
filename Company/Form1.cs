using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Company
{
    public partial class Form1 : Form
    {
        Boolean flag;
        Boolean flag2;

        List<ItemsPer> inserted_Items;
        List<string> nms = new List<string>();
        List<string> nms2 = new List<string>();

        CompanyMarketEntities db;
        public Form1()
        {
            InitializeComponent();
            flag = true;
            flag2 = true;
            db = new CompanyMarketEntities();
            inserted_Items = new List<ItemsPer>() ;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var whs = from d in db.WareHouses
                      select d.WH_Name;


            var suppemails = from s in db.Suppliers
                             select s.Supp_Email;

            var cusmails = from cus in db.Customers
                           select cus.Cus_Email;

            foreach (var cc in cusmails)
            {
                comboBox4.Items.Add(cc);
            }
            foreach (var i in whs)
            {
                comboBox1.Items.Add(i);
            }
            var item = from i in db.Items
                       select i.I_Code;
            foreach(var ii in item)
            {
                comboBox2.Items.Add(ii);
            }
            foreach(var ss in suppemails)
            {
                comboBox3.Items.Add(ss);
            }
        }
        private void button1_Click(object sender, EventArgs e) // add
        {
            if ( textBox1.Text!= null& textBox2.Text != null & textBox3.Text != null )
            {

                var whname = db.WareHouses.Find(textBox1.Text);
                if ( whname ==null)
                {
                    WareHouse wh = new WareHouse();
                    wh.WH_Name = textBox1.Text;
                    wh.WH_Address = textBox2.Text;
                    wh.WH_Maneger = textBox3.Text;
                    db.WareHouses.Add(wh);
                    db.SaveChanges();
                    textBox1.Text = textBox2.Text = textBox3.Text = "";
                }
                else
                {
                    MessageBox.Show("the warehouse name is exist");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) // update 
        {
                if (textBox4.Text != "" && textBox5.Text != "")
                {
                    WareHouse w = db.WareHouses.Find(comboBox1.SelectedItem);
                    w.WH_Maneger = textBox4.Text;
                    w.WH_Address = textBox5.Text;
                    db.SaveChanges();
                    MessageBox.Show("Edit succeeded");
                }
                else
                {
                    MessageBox.Show("Please Complete all info about WareHouse");
                }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)//to select the data of the selected wareHouse 
        {
                string selectedItem = comboBox1.SelectedItem.ToString();

                var wareInfo = (from d in db.WareHouses
                                where d.WH_Name == selectedItem
                                select d).First();
                textBox5.Text = wareInfo.WH_Address;
                textBox4.Text = wareInfo.WH_Maneger;
        }
        private void updateWareHouse_Click(object sender, EventArgs e)
        {
           
        }

        /********************************** item ******************************************** */
        private void AddItem_btn_Click(object sender, EventArgs e)
        {

            if (textBox_I_Code.Text != "" & textBoxI_Name.Text !="")
            {
                Item I = new Item(); 
                var ICode = db.Items.Find(int.Parse(textBox_I_Code.Text));
                
                if (ICode == null )
                {
                    I.I_Code = int.Parse(textBox_I_Code.Text);
                    I.I_Name = textBoxI_Name.Text;
                    foreach (var m in checkedListBox2.CheckedItems)
                    {
                        Item_Measure p = new Item_Measure();
                        p.I_Code = int.Parse(textBox_I_Code.Text);
                        MessageBox.Show(m.ToString());
                        p.Measure = m.ToString();
                        db.Item_Measure.Add(p);
                    }

                    db.Items.Add(I);
                    db.SaveChanges();
                    textBox_I_Code.Text = textBoxI_Name.Text = "";
                    comboBox2.Items.Clear();
                    var item = from i in db.Items
                               select i.I_Code;
                    foreach (var ii in item)
                    {
                        comboBox2.Items.Add(ii);
                    }
                }
                else
                {
                    MessageBox.Show("the Item id is exist");
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItem = comboBox2.SelectedItem.ToString();
            int selectedItem2 = int.Parse(selectedItem);
            var iteminfo = (from d in db.Items
                            where d.I_Code == selectedItem2
                            select d).First();
            textBox10.Text = iteminfo.I_Name;

            CheckBox[] ch = new CheckBox[5];

            ch[0] = checkBox1;
            ch[1] = checkBox2;
            ch[2] = checkBox3;
            ch[3] = checkBox4;
            ch[4] = checkBox5;

            //for (int xy = 0; xy < checkedListBox3.Items.Count; xy++)
            //{
            //    for (int x = 0; x < 5; x++)
            //    {
            //        if (checkedListBox3.Items[xy].ToString() == ch[x].Text)
            //        {
            //            ch[x].Checked = true;
            //        }
            //    }
            //}
        }
        private void button4_Click(object sender, EventArgs e)   // update item 
        {
            if ( comboBox2.Text!= "" && textBox10.Text!="")
            {
                Item I = db.Items.Find(comboBox2.SelectedItem);
                I.I_Name = textBox10.Text;
                db.SaveChanges();
                MessageBox.Show("Edit succeeded");
            }
            else
            {
                MessageBox.Show("Please Complete all info about Item");
            }
        }

        private void tabPage8_Click(object sender, EventArgs e)
        {

        }
        /*********************** Supplier ****************************/

        private void AddSupp_Click(object sender, EventArgs e)
        {
            string supEmail = textSuppEmail.Text;
            string supName = textBoxSuppName.Text;
            string supPhone = textSuppPhone.Text;
            string supMobile = textSuppMobile.Text;
            string supFax = textSuppFax.Text;
            string supSite = textSuppSite.Text;
            if (supEmail != "" && supName != ""&& supMobile != "" && supFax != "" && supSite != "")
            {
                var subFound = db.Suppliers.Find(supEmail);
                if (subFound == null)// PK not founded
                {
                    Supplier s = new Supplier();
                    s.Supp_Email = supEmail;
                    s.Supp_Name = supName;
                    s.Supp_Fax = supPhone;
                    s.Supp_Site = supSite;
                    s.Supp_Mobile = int.Parse(supMobile);
                    s.Supp_phone = int.Parse(supPhone);
                    db.Suppliers.Add(s);
                    db.SaveChanges();
                    MessageBox.Show("Supplier added succesfully");
                    textSuppEmail.Text=textBoxSuppName.Text=textSuppPhone.Text=textSuppMobile.Text=textSuppFax.Text=textSuppSite.Text="";

                    comboBox3.Items.Clear();
                    var supmails = from sup in db.Suppliers
                                   select sup.Supp_Email;
                    foreach (var ss in supmails)
                    {
                        comboBox3.Items.Add(ss);
                    }
                }
                else
                {
                    MessageBox.Show("The PK is duplicated");
                }
            }
            else
            {
                    MessageBox.Show("some info is missed ,please complete them");
            }
        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem != null)
            {
                var selected = db.Suppliers.Find(comboBox3.SelectedItem);
                text_suppName.Text = selected.Supp_Name;
                text_suppMob.Text = selected.Supp_Mobile.ToString();
                text_suppSite.Text = selected.Supp_Site;
                textBox_suppPhone.Text = selected.Supp_phone.ToString();
                text_suppFax.Text = selected.Supp_Fax;
            }
        }

        private void UpdateSupp_Click(object sender, EventArgs e)
        {
            if (text_suppName.Text != "" && textBox_suppPhone.Text != "" && text_suppMob.Text != "" && text_suppSite.Text != "" && text_suppFax.Text != "")
            {
                var supplier = db.Suppliers.Find(comboBox3.SelectedItem);  // using email PK
                supplier.Supp_Name = text_suppName.Text;
                supplier.Supp_phone = int.Parse(textBox_suppPhone.Text);
                supplier.Supp_Mobile = int.Parse(text_suppMob.Text);
                supplier.Supp_Site = text_suppSite.Text;
                supplier.Supp_Fax = text_suppFax.Text;
                db.SaveChanges();
                MessageBox.Show("Edit succeeded");
                text_suppName.Text = textBox_suppPhone.Text = text_suppMob.Text= text_suppSite.Text = text_suppFax.Text = "";
                }
            else
            {
                MessageBox.Show("Please Complete all info about Supplier");
            }
        }
        /*********************** Customer ***************************/
        private void AddCus_Click(object sender, EventArgs e)
        {
            string cusEmail = textcusEmail.Text;
            string cusName = textcusName.Text;
            string cusPhone = textcusPhone.Text;
            string cusMobile = textcusMob.Text;
            string cusFax = textcusFax.Text;
            string cusSite = textcusSite.Text;
            if (cusEmail != "" && cusName != "" && cusMobile != "" && cusFax != "" && cusSite != "")
            {
                var cusFound = db.Customers.Find(cusEmail);
                if (cusFound == null)// PK not founded
                {
                    Customer c = new Customer();
                    c.Cus_Email = cusEmail;
                    c.Cus_Name = cusName;
                    c.Cus_Fax = cusPhone;
                    c.Cus_Site = cusSite;
                    c.Cus_Mobile = int.Parse(cusMobile);
                    c.Cus_Phone = int.Parse(cusPhone);
                    db.Customers.Add(c);
                    db.SaveChanges();
                    MessageBox.Show("Customer added succesfully");
                    textcusEmail.Text = textcusName.Text = textcusPhone.Text = textcusMob.Text = textcusFax.Text = textcusSite.Text = "";

                    comboBox4.Items.Clear();
                    var cusmails = from cus in db.Customers
                                   select cus.Cus_Email;
                    foreach (var cc in cusmails)
                    {
                        comboBox4.Items.Add(cc);
                    }
                }
                else
                {
                    MessageBox.Show("The PK is duplicated");
                }
            }
            else
            {
                MessageBox.Show("some info is missed ,please complete them");
            }
        }
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.SelectedItem != null)
            {
                var selected = db.Customers.Find(comboBox4.SelectedItem);
                text_cusName.Text = selected.Cus_Name;
                text_cusMob.Text = selected.Cus_Mobile.ToString();
                text_cusSite.Text = selected.Cus_Site;
                text_cusPhone.Text = selected.Cus_Phone.ToString();
                text_cusFax.Text = selected.Cus_Fax;
            }
        }
        private void UpdateCus_Click(object sender, EventArgs e)
        {
            if (text_cusName.Text != ""&& text_cusMob.Text != "" && text_cusSite.Text != "" && text_cusFax.Text != "")
            {
                var cust = db.Customers.Find(comboBox4.SelectedItem);  // using email PK
                cust.Cus_Name = text_cusName.Text;
                cust.Cus_Phone = int.Parse(text_cusPhone.Text);
                cust.Cus_Mobile = int.Parse(text_cusMob.Text);
                cust.Cus_Site = text_cusSite.Text;
                cust.Cus_Fax = text_cusFax.Text;
                db.SaveChanges();
                MessageBox.Show("Edit succeeded");
                text_cusName.Text = text_cusPhone.Text = text_cusMob.Text = text_cusSite.Text = text_cusFax.Text = "";
            }
            else
            {
                MessageBox.Show("Please Complete all info about Customer");
            }
        }

        /************************** import permission ****************************/
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.TabIndex == 5)
            {
                var whs = from d in db.WareHouses
                          select d.WH_Name;

                var item = from i in db.Items
                           select i.I_Name;

                var suppemails = from s in db.Suppliers
                                 select s.Supp_Name;

                comboBoxP.Items.Clear();
                comboBox13.Items.Clear();
                comboBox14.Items.Clear();
                comboBoxPerNumI.Items.Clear();
                var pernum = (from p in db.Permetion_Import
                             select p.Per_Num_I).Distinct();

                foreach (var pp in pernum)
                {
                    comboBoxPerNumI.Items.Add(pp);
                }

                foreach (var i in whs)
                {
                    comboBox14.Items.Add(i);
                }
                foreach (var ii in item)
                {
                    comboBoxP.Items.Add(ii);
                }
                foreach (var ss in suppemails)
                {
                    comboBox13.Items.Add(ss);
                }
            }

            if (tabControl1.SelectedIndex == 5) // out from our warehouse
            {
                var whs = from d in db.WareHouses
                          select d.WH_Name;


                var suppNms = from s in db.Customers
                                 select s.Cus_Name;

                comboBoxWH_NME.Items.Clear();
                comboBoxSuppE.Items.Clear();

               
                var pernum = (from p in db.Permetion_Export
                              select p.Per_Num_E).Distinct();

                foreach (var pp in pernum)
                {
                    comboBoxPerNumE.Items.Add(pp);
                }

                foreach (var i in whs)
                {
                    comboBoxWH_NME.Items.Add(i);
                }
                foreach (var ss in suppNms)
                {
                    comboBoxSuppE.Items.Add(ss);
                }
            }

            if (tabControl1.SelectedIndex == 6) // 
            {
                from.Items.Clear();
                to.Items.Clear();
                pro.Items.Clear();
                Qty.Text = "";
                var whs = from d in db.WareHouses
                          select d.WH_Name;

                foreach (var i in whs)
                {
                    from.Items.Add(i);
                }
            }

            if (tabControl1.SelectedIndex==7)
            {
                comboBox7.Items.Clear();
                comboBox6.Items.Clear();
                textBox7.Text = textBox9.Text = comboBox6.Text = "";
                var whs = (from d in db.item_WareHouse
                           select d.WH_Name).Distinct();

                var prod = (from d in db.item_WareHouse
                            select d.Item.I_Name).Distinct();
                foreach (var i in whs)
                {
                    comboBox6.Items.Add(i);
                }
                foreach (var p in prod)
                {
                    comboBox7.Items.Add(p);
                }

            }
        }
        private void button5_Click(object sender, EventArgs e) // open panel
        {
            if (textPerNum.Text != ""&&comboBox13.Text !="" && comboBox14.Text!="")
            {
                panel2.Enabled = true;
            }
            else
            {
                panel2.Enabled = false;
            }
        }
        private void AddI_Click(object sender, EventArgs e)
        {
            var found=  db.Permetion_Import.Select(i=>i.Per_Num_I== int.Parse(textPerNum.Text));

            if (found !=null)
            {
                if (comboBoxP.Text != "" && textBox6.Text != "")
                {
                    ItemsPer ii = new ItemsPer();
                    ii.ProductName = comboBoxP.SelectedItem.ToString();
                    ii.ItemQuantity = int.Parse(textBox6.Text) ;
                    ii.ProdDate = dateTimePicker1.Value;
                    ii.ExpiDate = dateTimePicker2.Value;
                    listBox1.Items.Add(ii.ProductName +" : "+ii.ItemQuantity );
                    inserted_Items.Add(ii);
                    textBox6.Text = comboBoxP.Text = "";
                }
                else
                {
                    MessageBox.Show(" missing fields required !! ");
                }
            }
            else
            {
                MessageBox.Show(" permission number is exist ! ");
            }
        }
        private void AddImpPer_Click(object sender, EventArgs e)
        {
            if (textPerNum.Text != ""  && comboBox13.Text != "" && comboBox14.Text != "" && listBox1.Items.Count!=0)
            {
                // per num    -- i code -- supp email --  per date -- whname
                //int I_IDD = db.Items.Where(i => i.I_Name == comboBoxP.Text).Select(i => i.I_Code).First();
                // per num -- icode -- suppmail , wh nm , prod dt , exp dt ,quan
                foreach(var d in inserted_Items)
                {
                    var emailSupp = db.Suppliers.Where(i => i.Supp_Name == comboBox13.Text).Select(i => i.Supp_Email);
                    int I_ID = db.Items.Where(i => i.I_Name == d.ProductName).Select(i => i.I_Code).First();
                   
                    Permetion_Import PI = new Permetion_Import();
                    PI.Per_Num_I = int.Parse(textPerNum.Text);
                    PI.Supp_Email = emailSupp.First();
                    PI.I_Code = I_ID;
                    PI.WH_Name = comboBox14.Text;
                    PI.Per_Date_I = dateTimePicker5.Value;
                    db.Permetion_Import.Add(PI);

                    Import_Quantity IQ = new Import_Quantity();
                    IQ.Per_Num_I = int.Parse(textPerNum.Text);
                    IQ.Supp_Email = emailSupp.First();
                    IQ.WH_Name = comboBox14.Text;
                    IQ.I_Code = I_ID;
                    IQ.In_Quantity = d.ItemQuantity;
                    IQ.Prod_Date = d.ProdDate;
                    IQ.Expi_Date = d.ExpiDate;
                    db.Import_Quantity.Add(IQ);

                    var found2 = (from dd in db.item_WareHouse
                                 where (dd.I_Code == I_ID && dd.WH_Name == comboBox14.Text)
                                 select dd).FirstOrDefault();


                   var found=  db.item_WareHouse.Where(i => i.I_Code == I_ID && i.WH_Name == comboBox14.Text).Select(i=>i).FirstOrDefault();

                    if (found != null)
                    {
                        int q = found.Quantity.Value;
                        db.item_WareHouse.Remove(found);
                        item_WareHouse IWH = new item_WareHouse();
                        IWH.I_Code = I_ID;
                        IWH.WH_Name = comboBox14.Text;
                        IWH.Quantity = d.ItemQuantity+q;
                        db.item_WareHouse.Add(IWH);
                    }
                    else
                    {
                        item_WareHouse IWH = new item_WareHouse();
                        IWH.I_Code = I_ID ;
                        IWH.WH_Name = comboBox14.Text;
                        IWH.Quantity = d.ItemQuantity;
                        db.item_WareHouse.Add(IWH);
                    }
                }
                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    MessageBox.Show(" mmm ");
                }
                
                MessageBox.Show("Permission added succesfully");
                textPerNum.Text = comboBoxP.Text = comboBox13.Text = comboBox14.Text =  "";
                inserted_Items.Clear();
                listBox1.Items.Clear();
                
                comboBoxPerNumI.Items.Clear();
                    var pernum = (from p in db.Permetion_Import
                                 select p.Per_Num_I);
                    var filtered = pernum.Distinct();
                    foreach(var pp in filtered)
                    {
                        comboBoxPerNumI.Items.Add(pp);
                    }
                }
                else
                {
                    MessageBox.Show("missing field ");
                }
        }
        private void updatePerI_Click(object sender, EventArgs e)
        {
            if (comboBoxPerNumI.Text != "" && comboBox8.Text != "" && comboBox9.Text != "")
            {
                flag = true;

                int x = 0;
                foreach (var u in inserted_Items)// inserted item have the value i made change on it
                {
                    //his for  warhouse_Item
                    int iCode = (db.Items.Where(i => i.I_Name == u.ProductName).Select(i => i.I_Code)).First();
                    var there=db.item_WareHouse.Where(i=>i.I_Code==iCode&&i.WH_Name== comboBox9.Text).Select(i => i).FirstOrDefault();
                    string edit = listBox2.SelectedItem.ToString();
                    var oldIcode = db.Items.Where(i => i.I_Name==u.oldRecord).Select(i => i.I_Code).First();
                    var oldRecord = db.item_WareHouse.Where(i => i.I_Code == oldIcode && i.WH_Name == comboBox9.Text).Select(i => i).First();
                    db.item_WareHouse.Remove(oldRecord);
                    
                    if (there != null)   //there is element ..
                    {
                        var oldQ=db.item_WareHouse.Where(i=>i.I_Code==iCode).Select(i=>i.Quantity).First();
                        int newQ = u.ItemQuantity +(int) oldQ;
                        there.Quantity = newQ;
                    }
                    else
                    {
                        item_WareHouse newRecord = new item_WareHouse();
                        newRecord.I_Code = iCode;
                        newRecord.WH_Name = comboBox9.Text;
                        newRecord.Quantity = u.ItemQuantity;
                        db.item_WareHouse.Add(newRecord);
                    }

                    // QI

                    var prevRecord = db.Import_Quantity.AsEnumerable().Where(i => i.I_Code == oldIcode && i.Per_Num_I == int.Parse(comboBoxPerNumI.Text)).Select(i => i).FirstOrDefault();
                    db.Import_Quantity.Remove(prevRecord);
                    Import_Quantity NewRec = new Import_Quantity();
                    NewRec.In_Quantity += u.ItemQuantity;
                    NewRec.I_Code = iCode;
                    NewRec.WH_Name = comboBox9.Text;
                    string email = db.Suppliers.Where(i => i.Supp_Name == comboBox8.Text).Select(i => i.Supp_Email).FirstOrDefault();
                    NewRec.Supp_Email = email;
                    NewRec.Per_Num_I = int.Parse(comboBoxPerNumI.Text);
                    NewRec.Expi_Date = u.ExpiDate;
                    NewRec.Prod_Date = u.ProdDate;
                    db.Import_Quantity.Add(NewRec);

                    // PI

                    var PIPrev = db.Permetion_Import.AsEnumerable().Where(i=>i.I_Code == oldIcode && i.Per_Num_I == int.Parse(comboBoxPerNumI.Text)).Select(i => i).FirstOrDefault();
                    db.Permetion_Import.Remove(PIPrev);
                    Permetion_Import NewPer = new Permetion_Import();
                    NewPer.Supp_Email = email;
                    NewPer.Per_Num_I = int.Parse(comboBoxPerNumI.Text);
                    NewPer.I_Code = iCode;
                    NewPer.WH_Name = comboBox9.Text;
                    NewPer.Per_Date_I = dateTimePicker6.Value;
                    db.Permetion_Import.Add(NewPer);
                    x++;
                }
                db.SaveChanges();
                inserted_Items.Clear();
                nms.Clear();
                x = 0;
            }
        }
        private void comboBoxPerNumI_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxPerNumI.SelectedItem != null)
            {
                comboBox5.Items.Clear();
                comboBox8.Items.Clear();
                comboBox9.Items.Clear();

                var whs = from d in db.WareHouses
                          select d.WH_Name;
                
                var suppemails = from s in db.Suppliers
                                 select s.Supp_Name;

                foreach (var i in whs)
                {
                    comboBox9.Items.Add(i);
                }
                foreach (var ss in suppemails)
                {
                    comboBox8.Items.Add(ss);
                }

                int per_id = (int)comboBoxPerNumI.SelectedItem;
                MessageBox.Show(comboBoxPerNumI.SelectedItem.ToString());
                var suppEmail = (from ii in db.Permetion_Import
                                 where ii.Per_Num_I == per_id
                                 select ii.Supp_Email).First();

                string suppNm = (db.Suppliers.Find(suppEmail)).Supp_Name.ToString();
                string WH_NM = db.Permetion_Import.Where(i => i.Per_Num_I == per_id).Select(i => i.WH_Name).First();
                var PerDate = db.Permetion_Import.Where(i => i.Per_Num_I == per_id).Select(i => i.Per_Date_I).First();
                var dataInfo = db.Import_Quantity.Where(i => i.Per_Num_I == per_id).Select(i => i.I_Code);

                comboBox8.Text = suppNm;
                comboBox9.Text = WH_NM;
                dateTimePicker6.Value = PerDate.Value;

                listBox2.Items.Clear();
                foreach(var d in dataInfo)
                {
                    var dd = db.Items.Find(d).I_Name;
                    listBox2.Items.Add(dd);
                }
            }
        }
        private void EditI_Click(object sender, EventArgs e) // edit 
        {
            if(flag)
            {
                for (int i =0; i< listBox2.Items.Count;i++)
                {
                    nms.Add(listBox2.Items[i].ToString());
                }
                flag = false;
            }
            
            string perNum = comboBoxPerNumI.SelectedItem.ToString();
            if (perNum != "")
            {
                if (comboBox5.Text != "" && textBox8.Text != "")
                {
                    int p = 0;
                    ItemsPer ii = new ItemsPer();
                    ii.ProductName = comboBox5.SelectedItem.ToString();
                    ii.ItemQuantity = int.Parse(textBox8.Text);
                    ii.ProdDate = dateTimePicker4.Value;
                    ii.ExpiDate = dateTimePicker3.Value;
                    ii.oldRecord= listBox2.SelectedItem.ToString();
                    p = listBox2.SelectedIndex;
                    listBox2.Items[p] = comboBox5.SelectedItem.ToString();
                    inserted_Items.Add(ii);
                    textBox8.Text = comboBox5.Text = "";
                }
                else
                {
                    MessageBox.Show(" missing fields required !! ");
                }
            }
            else
            {
                MessageBox.Show(" permission number is exist ! ");
            }
        }
        private void listBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (listBox2.SelectedItem == null)
            {
            }
            else
            {
                string selected = listBox2.SelectedItem.ToString();
                int pernum = (int)comboBoxPerNumI.SelectedItem;
                int id = (db.Items.Where(i => i.I_Name == selected).Select(i => i.I_Code)).First();
                var data = from d in db.Import_Quantity
                           where (d.Per_Num_I == pernum && d.I_Code == id)
                           select d.In_Quantity;

                var outdate = from d in db.Import_Quantity
                              where (d.Per_Num_I == pernum && d.I_Code == id)
                              select d.Expi_Date;

                var indate = from d in db.Import_Quantity
                             where (d.Per_Num_I == pernum && d.I_Code == id)
                             select d.Prod_Date;
                

                comboBox5.Items.Clear();

                var items = (from ii in db.Items
                           select ii.I_Name);

                List<string> item = new List<string>();
                foreach (var i in items)
                {
                    item.Add(i.ToString());
                }

                HashSet<string> hs = new HashSet<string>();
                for (int i = 0; i < listBox2.Items.Count; i++)
                {
                    hs.Add(listBox2.Items[i].ToString());
                }

                for (int i =0; i <item.Count; i++)
                {
                    if(!hs.Contains(item[i]))        // false true 
                    {
                        comboBox5.Items.Add(item[i]);
                    }
                }
                    comboBox5.Text = selected;
                if (data.FirstOrDefault()!=0 && indate.FirstOrDefault() !=null && outdate.FirstOrDefault()!=null)
                {
                    textBox8.Text = (data.FirstOrDefault()).ToString();
                    dateTimePicker4.Value = (indate.FirstOrDefault()).Value;
                    dateTimePicker3.Value = (outdate.FirstOrDefault()).Value;
                }
            }
        }

        /************************** Export permission ****************************/
        private void AE_Click(object sender, EventArgs e) // Add Export
        {
            if (textBoxPerNE.Text != "" && comboBoxWH_NME.Text != "" && comboBoxSuppE.Text != "")
            {
                panel5.Enabled = true;
            }
            else
            {
                panel5.Enabled = false;
            }
        }
        private void AddPerL_Click(object sender, EventArgs e)
        {
            var found = db.Permetion_Export.Select(i => i.Per_Num_E == int.Parse(textBoxPerNE.Text));
            if (found != null)
            {
                if (comboBoxINM_E.Text != "" && textBoxQE.Text != "")
                {
                    ItemsPer ii = new ItemsPer();
                    ii.ProductName = comboBoxINM_E.SelectedItem.ToString();
                    ii.ItemQuantity = int.Parse(textBoxQE.Text);
                    ii.ProdDate = dateTimePicker11.Value;
                    ii.ExpiDate = dateTimePicker10.Value;
                    listBox4.Items.Add(ii.ProductName + " : " + ii.ItemQuantity);
                    inserted_Items.Add(ii);
                    textBoxQE.Text = comboBoxINM_E.Text = "";
                }
                else
                {
                    MessageBox.Show(" missing fields required !! ");
                }
            }
            else
            {
                MessageBox.Show(" permission number is exist ! ");
            }
        }
        private void AddPerE_Click(object sender, EventArgs e) // Add to database
        {
            if ( inserted_Items.Count>0  && textBoxPerNE.Text != "" && comboBoxWH_NME.Text != "" && comboBoxSuppE.Text != "" && listBox4.Items.Count != 0)
            {
                foreach (var d in inserted_Items)
                {
                    var emailCus = db.Customers.Where(i => i.Cus_Name == comboBoxSuppE.Text).Select(i => i.Cus_Email);
                    int I_ID = db.Items.AsEnumerable().Where(i => i.I_Name == d.ProductName).Select(i => i.I_Code).First();

                    var QuantityFounded = (db.item_WareHouse.Find( comboBoxWH_NME.Text, I_ID).Quantity) ;
                    if (QuantityFounded != null)     // there are items in wh
                    {
                        if (QuantityFounded.Value> d.ItemQuantity )
                        {
                            Permetion_Export PE = new Permetion_Export();
                            PE.Per_Num_E = int.Parse(textBoxPerNE.Text);
                            PE.Cus_Email = emailCus.First();
                            PE.I_Code = I_ID;
                            PE.WH_Name = comboBoxWH_NME.Text;
                            PE.Per_DateE = dateTimePicker12.Value;
                            db.Permetion_Export.Add(PE);

                            Export_Quantity EQ = new Export_Quantity();
                            EQ.Per_Num_E = int.Parse(textBoxPerNE.Text);
                            EQ.Cus_Email = emailCus.First();
                            EQ.WH_Name = comboBoxWH_NME.Text;
                            EQ.I_Code = I_ID;
                            EQ.Out_Quantity = d.ItemQuantity;
                            EQ.Prod_Date = d.ProdDate;
                            EQ.Expi_Date = d.ExpiDate;
                            db.Export_Quantity.Add(EQ);

                            var found = db.item_WareHouse.Where(i => i.I_Code == I_ID && i.WH_Name == comboBoxWH_NME.Text).Select(i => i).First();

                            //if (found != null)
                            //{
                                int q = found.Quantity.Value;  // old quantity 
                                db.item_WareHouse.Remove(found);
                                item_WareHouse IWH = new item_WareHouse();
                                IWH.I_Code = I_ID;
                                IWH.WH_Name = comboBoxWH_NME.Text;
                                if (q > d.ItemQuantity)
                                {
                                    IWH.Quantity = q - d.ItemQuantity;
                                }
                                db.item_WareHouse.Add(IWH);
             

                        }
                        else
                        {
                            MessageBox.Show(" there are no enouph items in the warehouse !! ");
                        }
                    }
                    else // no items
                    {
                        MessageBox.Show(" NOT FOUND !! ");
                    }




                   

                   
                }
                try
                {
                    db.SaveChanges();
                    MessageBox.Show("Permission added succesfully");
                }
                catch
                {
                    MessageBox.Show(" Can't Add to Database ");
                }
                textBoxPerNE.Text = comboBoxINM_E.Text = comboBoxWH_NME.Text = comboBoxSuppE.Text = "";
                inserted_Items.Clear();
                listBox4.Items.Clear();

                comboBoxPerNumE.Items.Clear();
                var pernum = (from p in db.Permetion_Export
                              select p.Per_Num_E);
                var filtered = pernum.Distinct();
                foreach (var pp in filtered)
                {
                    comboBoxPerNumE.Items.Add(pp);
                }
            }
            else
            {
                MessageBox.Show("missing field ");
            }
        }
        private void comboBoxWH_NME_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxINM_E.Items.Clear();
            var item = from i in db.item_WareHouse
                       where i.WH_Name == comboBoxWH_NME.Text
                       select i.Item.I_Name;
            foreach (var ii in item)
            {
                comboBoxINM_E.Items.Add(ii);
            }
            comboBoxPerNumE.Items.Clear();
        }
        private void comboBoxPerNumE_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxPerNumE.SelectedItem != null)
            {
                comboBox_CusNM.Items.Clear();
                comboBox_WHNM.Items.Clear();
                comboBoxI_nm.Items.Clear();

                var whs = from d in db.WareHouses
                          select d.WH_Name;

                var suppemails = from s in db.Customers
                                 select s.Cus_Name;

                foreach (var i in whs)
                {
                    comboBox_WHNM.Items.Add(i);
                }
                foreach (var ss in suppemails)
                {
                    comboBox_CusNM.Items.Add(ss);
                }

                int per_id = (int)comboBoxPerNumE.SelectedItem;
               
                var suppEmail = (from ii in db.Permetion_Export
                                 where ii.Per_Num_E == per_id
                                 select ii.Cus_Email).First();

                string suppNm = (db.Customers.Find(suppEmail)).Cus_Name.ToString();
                string WH_NM = db.Permetion_Export.Where(i => i.Per_Num_E == per_id).Select(i => i.WH_Name).First();
                var PerDate = db.Permetion_Export.Where(i => i.Per_Num_E == per_id).Select(i => i.Per_DateE).First();
                var dataInfo = db.Export_Quantity.Where(i => i.Per_Num_E == per_id).Select(i => i.I_Code);

                comboBox_CusNM.Text = suppNm;
                comboBox_WHNM.Text = WH_NM;
                dateTimePicker9.Value = PerDate.Value;

                listBox3.Items.Clear();
                foreach (var d in dataInfo)
                {
                    var dd = db.Items.Find(d).I_Name;
                    listBox3.Items.Add(dd);
                }
            }
        }
        private void listBox3_MouseClick(object sender, MouseEventArgs e)
        {
            if (listBox3.SelectedItem == null)
            {
            }
            else
            {
                string selected = listBox3.SelectedItem.ToString();
                int pernum = (int)comboBoxPerNumE.SelectedItem;
                int id = (db.Items.Where(i => i.I_Name == selected).Select(i => i.I_Code)).First();
                var OutQ = from d in db.Export_Quantity
                           where (d.Per_Num_E == pernum && d.I_Code == id)
                           select d.Out_Quantity;

                var outdate = from d in db.Export_Quantity
                              where (d.Per_Num_E == pernum && d.I_Code == id)
                              select d.Expi_Date;

                var indate = from d in db.Export_Quantity
                             where (d.Per_Num_E == pernum && d.I_Code == id)
                             select d.Prod_Date;


                comboBoxI_nm.Items.Clear();

                var items = (from ii in db.item_WareHouse
                             where ii.WH_Name == comboBox_WHNM.Text 
                             select ii.Item.I_Name);

                List<string> item = new List<string>();
                foreach (var i in items)
                {
                    item.Add(i.ToString());
                }

                HashSet<string> hs = new HashSet<string>();
                for (int i = 0; i < listBox3.Items.Count; i++)
                {
                    hs.Add(listBox3.Items[i].ToString());
                }

                for (int i = 0; i < item.Count; i++)
                {
                    if (!hs.Contains(item[i]))        // false true 
                    {
                        comboBoxI_nm.Items.Add(item[i]);
                    }
                }
                comboBoxI_nm.Text = selected;
                if (OutQ.FirstOrDefault() != 0 && indate.FirstOrDefault() != null && outdate.FirstOrDefault() != null)
                {
                    textBoxQunE.Text = (OutQ.FirstOrDefault()).ToString();
                    dateTimePicker8.Value = (indate.FirstOrDefault()).Value;
                    dateTimePicker7.Value = (outdate.FirstOrDefault()).Value;
                }
            }
        }
        private void AddLE_Click(object sender, EventArgs e)  // edit to listbox
        {
            if (flag2)
            {
                for (int i = 0; i < listBox3.Items.Count; i++)
                {
                    nms2.Add(listBox3.Items[i].ToString());
                }
                flag2 = false;
            }

            string perNum = comboBoxPerNumE.SelectedItem.ToString();
            if (perNum != "")
            {
                if (comboBoxI_nm.Text != "" && textBoxQunE.Text != "")
                {
                    int p = 0;
                    ItemsPer ii = new ItemsPer();
                    ii.ProductName = comboBoxI_nm.SelectedItem.ToString();
                    ii.ItemQuantity = int.Parse(textBoxQunE.Text);
                    ii.ProdDate = dateTimePicker8.Value;
                    ii.ExpiDate = dateTimePicker7.Value;
                    ii.oldRecord = listBox3.SelectedItem.ToString();
                    p = listBox3.SelectedIndex;
                    listBox3.Items[p] = comboBoxI_nm.SelectedItem.ToString();
                    inserted_Items.Add(ii);
                    textBoxQunE.Text = comboBoxI_nm.Text = "";
                }
                else
                {
                    MessageBox.Show(" missing fields required !! ");
                }
            }
            else
            {
                MessageBox.Show(" permission number is exist ! ");
            }
        }
        private void UpdatePerE_Click(object sender, EventArgs e)
        {
            if (comboBoxPerNumE.Text != "" && comboBox_WHNM.Text != "" && inserted_Items.Count > 0)
            {
                flag = true;
                int x = 0;
                foreach (var u in inserted_Items)// inserted item have the value i made change on it
                {
                    var emailCus = db.Customers.Where(i => i.Cus_Name == comboBoxSuppE.Text).Select(i => i.Cus_Email);
                    int I_ID = db.Items.AsEnumerable().Where(i => i.I_Name == u.ProductName).Select(i => i.I_Code).First(); // new

                    var there = db.item_WareHouse.Where(i => i.I_Code == I_ID && i.WH_Name == comboBox_WHNM.Text).Select(i => i).FirstOrDefault();
                    string edit = listBox3.SelectedItem.ToString();
                    var oldIcode = db.Items.Where(i => i.I_Name == u.oldRecord).Select(i => i.I_Code).First();
                    var oldRecord = db.item_WareHouse.Where(i => i.I_Code == oldIcode && i.WH_Name == comboBox_WHNM.Text).Select(i => i).First();
                    var OldQ = db.Export_Quantity.AsEnumerable().Where(i => i.I_Code == oldIcode && i.Per_Num_E == int.Parse(comboBoxPerNumE.Text)).Select(i => i.Out_Quantity).FirstOrDefault();

                    if (there != null)   //there is element  on the table ..
                    {
                        var QuantityFounded = (db.item_WareHouse.Find(comboBox_WHNM.Text, I_ID).Quantity);
                        if (QuantityFounded != null)     // there are items in wh
                        {
                            if (u.ItemQuantity> OldQ)  //   ===> accepted edit
                            {
                                int diff = (u.ItemQuantity - OldQ);  
                                if (QuantityFounded >= diff)
                                {
                                    db.item_WareHouse.Remove(oldRecord);
                                    there.Quantity = QuantityFounded - diff;
                                }
                                else
                                {
                                    MessageBox.Show(" there are no enouph items in the warehouse !! ");
                                }
                            }
                            else if (u.ItemQuantity < OldQ)
                            {
                                int diff = (OldQ - u.ItemQuantity);
                                db.item_WareHouse.Remove(oldRecord);
                                there.Quantity = QuantityFounded + diff;
                            }
                            else
                            {
                                MessageBox.Show("Same Quantity");
                            }
                        }
                    }
                    else
                    {
                        item_WareHouse newRecord = new item_WareHouse();
                        newRecord.I_Code = I_ID;
                        newRecord.WH_Name = comboBox_WHNM.Text;
                        newRecord.Quantity = u.ItemQuantity;
                        db.item_WareHouse.Add(newRecord);
                    }

                    // QI

                    var prevRecord = db.Export_Quantity.AsEnumerable().Where(i => i.I_Code == oldIcode && i.Per_Num_E == int.Parse(comboBoxPerNumE.Text)).Select(i => i).FirstOrDefault();
                    db.Export_Quantity.Remove(prevRecord);
                    Export_Quantity NewRec = new Export_Quantity();
                    NewRec.Out_Quantity += u.ItemQuantity;
                    NewRec.I_Code = I_ID;
                    NewRec.WH_Name = comboBox_WHNM.Text;
                    string email = db.Customers.Where(i => i.Cus_Name == comboBox_CusNM.Text).Select(i => i.Cus_Email).FirstOrDefault();
                    NewRec.Cus_Email = email;
                    NewRec.Per_Num_E = int.Parse(comboBoxPerNumE.Text);
                    NewRec.Expi_Date = u.ExpiDate;
                    NewRec.Prod_Date = u.ProdDate;
                    db.Export_Quantity.Add(NewRec);

                    // PE

                    var PIPrev = db.Permetion_Export.AsEnumerable().Where(i => i.I_Code == oldIcode && i.Per_Num_E == int.Parse(comboBoxPerNumE.Text)).Select(i => i).FirstOrDefault();
                    db.Permetion_Export.Remove(PIPrev);
                    Permetion_Export NewPer = new Permetion_Export();
                    NewPer.Cus_Email = email;
                    NewPer.Per_Num_E = int.Parse(comboBoxPerNumE.Text);
                    NewPer.I_Code = I_ID;
                    NewPer.WH_Name = comboBox_WHNM.Text;
                    NewPer.Per_DateE = dateTimePicker9.Value;
                    db.Permetion_Export.Add(NewPer);

                    x++;
                }
                try
                {
                    db.SaveChanges();
                    inserted_Items.Clear();
                    nms.Clear();
                    MessageBox.Show("Successfully done");
                }
                catch
                {
                    MessageBox.Show("mmm");
                }
                x = 0;
            }
        }
        /***********************************************************************/
        private void from_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected_whs = from.SelectedItem.ToString();
            to.Items.Clear();
            to.Text = "";
            pro.Items.Clear();
            Qty.Text = pro.Text = "";
            var All_whs = (from ii in db.WareHouses
                           select ii.WH_Name);

            var All_Items = (from ii in db.item_WareHouse
                             where ii.WH_Name == selected_whs 
                             select ii.Item.I_Name);

            foreach ( var d in All_whs)
            {
                if (selected_whs!=d)
                {
                    to.Items.Add(d);
                }
            }
            foreach( var d in All_Items)
            {
                pro.Items.Add(d);
            }

        }

        private void button3_Click(object sender, EventArgs e) // etmam elt7weel 
        {
            if (to.Text != "" && from.Text != "" && Qty.Text !="" && pro.Text != ""  )
            {
                var iCode = db.Items.Where(i => i.I_Name == pro.Text).Select(i => i.I_Code).FirstOrDefault();
                var StoreQ = db.item_WareHouse.Where(i => i.WH_Name == from.Text && i.I_Code == iCode).Select(i => i.Quantity).FirstOrDefault();
                if (int.Parse(Qty.Text)< StoreQ)
                {
                    Move_To mv = new Move_To();
                    mv.I_Code = iCode;
                    mv.ToWH_Nm = to.Text;
                    mv.FromWH_Nm = from.Text;
                    mv.Quantity = int.Parse(Qty.Text);
                    mv.Move_Date = dateTimePicker13.Value;
                    mv.Production_Date = dateTimePicker14.Value;
                    mv.Expire_Date = dateTimePicker15.Value;
                    db.Move_To.Add(mv);

                    var FromWh = db.item_WareHouse.Where(i => i.I_Code == iCode && i.WH_Name == from.Text).Select(i => i).First();
                    FromWh.Quantity = StoreQ - int.Parse(Qty.Text);

                    var ToWh = db.item_WareHouse.Where(i => i.I_Code == iCode && i.WH_Name == to.Text).Select(i => i).First();
                    if (ToWh !=null)  // founded item 
                    {
                        int q = ToWh.Quantity.Value;
                        ToWh.Quantity = int.Parse(Qty.Text)+q ;
                    }
                    else
                    {
                        item_WareHouse newRecord = new item_WareHouse();
                        newRecord.I_Code = iCode;
                        newRecord.WH_Name = to.Text;
                        newRecord.Quantity = int.Parse(Qty.Text) ;
                        db.item_WareHouse.Add(newRecord);
                    }
                    db.SaveChanges();
                    MessageBox.Show("successfully");
                }
                else
                {
                    MessageBox.Show(" no enouph quantity !!");
                }
            }
            else
            {
                MessageBox.Show(" Missing fields  ");
            }
        }

        private void pro_SelectedIndexChanged(object sender, EventArgs e)
        {
            var iCode = db.Items.Where(i => i.I_Name == pro.Text).Select(i => i.I_Code).FirstOrDefault();
            var rec = db.Import_Quantity.Where(i => i.WH_Name == from.Text && i.I_Code == iCode).Select(i => i).First(); 
            dateTimePicker14.Value = rec.Prod_Date.Value ;
            dateTimePicker15.Value = rec.Expi_Date.Value;

        }

        private void tabPage19_Click(object sender, EventArgs e)
        {

        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            var whn = db.GetWHInfo(comboBox6.Text);
            dataGridView1.DataSource = whn;
            var w = db.WareHouses.Find(comboBox6.Text);
            textBox7.Text = w.WH_Address.ToString();
            textBox9.Text = w.WH_Maneger.ToString();
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            var pro = db.ProductInfor(comboBox7.Text);
            dataGridView2.DataSource = pro;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var dt = db.Movement(dateTimePicker16.Value, dateTimePicker17.Value);
            dataGridView3.DataSource = dt ;
        }

        private void button8_Click(object sender, EventArgs e)
        {

            if (textBox11.Text!="")
            {
                try
                {
                    var dt = db.R4(int.Parse(textBox11.Text));
                    dataGridView5.DataSource = dt;
                }
                catch
                {
                    MessageBox.Show("you must enter a number!!");
                }
            }
            else
            {
                MessageBox.Show("you must enter a number ");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(textBox12.Text!="")
            {
                var ss = db.R5(int.Parse(textBox12.Text));
                dataGridView4.DataSource = ss;
            }
            else
            {
                MessageBox.Show(" you must enter a num");
            }
        }
    }

}
