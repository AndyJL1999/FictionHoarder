using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FictionHoarderWPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class ReadingPage
    {
        public ReadingPage()
        {
            InitializeComponent();
            AddText(textSum);
        }

        public void AddText(TextBlock box)
        {
            box.Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Sagittis eu volutpat odio facilisis. Egestas dui id ornare arcu odio ut sem." +
                " Sem et tortor consequat id porta nibh. Odio tempor orci dapibus ultrices in. Lorem mollis aliquam ut porttitor leo a diam. Urna id volutpat lacus laoreet. Aliquam sem et tortor consequat id porta nibh venenatis." +
                " Congue nisi vitae suscipit tellus mauris a. Velit sed ullamcorper morbi tincidunt ornare massa. Egestas diam in arcu cursus euismod.Maecenas accumsan lacus vel facilisis volutpat est velit egestas.Aliquam vestibulum morbi blandit cursus risus." +
                " Ultricies tristique nulla aliquet enim tortor at auctor. Aliquam malesuada bibendum arcu vitae.Tristique sollicitudin nibh sit amet.Pellentesque id nibh tortor id.Sed vulputate mi sit amet mauris commodo quis imperdiet massa." +
                " Dolor sit amet consectetur adipiscing elit duis tristique sollicitudin nibh. Mattis aliquam faucibus purus in massa tempor nec.Dolor sit amet consectetur adipiscing elit ut aliquam purus sit. Duis ultricies lacus sed turpis tincidunt id aliquet risus.Mauris ultrices eros in cursus turpis massa." +
                "Elit sed vulputate mi sit amet. Tincidunt nunc pulvinar sapien et ligula ullamcorper malesuada. Faucibus a pellentesque sit amet porttitor eget dolor morbi non. Habitant morbi tristique senectus et netus et malesuada. Pharetra et ultrices neque ornare aenean. Venenatis a condimentum vitae sapien pellentesque habitant morbi tristique." +
                "Eros in cursus turpis massa.Gravida neque convallis a cras.Eu feugiat pretium nibh ipsum consequat nisl vel. Non arcu risus quis varius quam quisque id diam vel. Tellus orci ac auctor augue mauris. Neque egestas congue quisque egestas diam in arcu cursus. Congue quisque egestas diam in. Tortor aliquam nulla facilisi cras fermentum odio eu. " +
                "Sed vulputate odio ut enim blandit volutpat maecenas volutpat blandit. Nulla facilisi morbi tempus iaculis urna id volutpat. Lectus arcu bibendum at varius vel pharetra vel turpis nunc. Vitae aliquet nec ullamcorper sit amet. Massa vitae tortor condimentum lacinia quis. Etiam dignissim diam quis enim lobortis scelerisque." +
                "Augue interdum velit euismod in. Mattis rhoncus urna neque viverra justo nec.Massa sed elementum tempus egestas sed sed risus pretium.Posuere ac ut consequat semper viverra. Diam phasellus vestibulum lorem sed risus ultricies tristique nulla.Eget nunc lobortis mattis aliquam faucibus. " +
                "Mi eget mauris pharetra et ultrices neque ornare. Lobortis elementum nibh tellus molestie.Erat velit scelerisque in dictum non consectetur a erat.Viverra aliquet eget sit amet tellus cras adipiscing enim eu. Sit amet consectetur adipiscing elit.Ut faucibus pulvinar elementum integer enim neque volutpat ac tincidunt. " +
                "Tellus id interdum velit laoreet id. Non consectetur a erat nam at lectus urna duis.Adipiscing enim eu turpis egestas pretium aenean pharetra. Dictumst quisque sagittis purus sit amet volutpat consequat mauris nunc. Quam elementum pulvinar etiam non quam lacus suspendisse faucibus interdum. Ut eu sem integer vitae justo eget magna fermentum.Faucibus ornare suspendisse sed nisi lacus sed.Senectus et netus et malesuada fames ac." +
                "Orci a scelerisque purus semper.Eget gravida cum sociis natoque penatibus et magnis dis.Diam quam nulla porttitor massa id neque aliquam. Odio pellentesque diam volutpat commodo sed egestas egestas fringilla phasellus. Urna porttitor rhoncus dolor purus non. Turpis egestas maecenas pharetra convallis." +
                "Non diam phasellus vestibulum lorem.Tortor dignissim convallis aenean et tortor at risus viverra.Malesuada fames ac turpis egestas sed tempus urna et.Ultrices vitae auctor eu augue ut lectus.Faucibus vitae aliquet nec ullamcorper sit amet risus. Gravida rutrum quisque non tellus orci ac auctor augue mauris.";
        }
    }
}
