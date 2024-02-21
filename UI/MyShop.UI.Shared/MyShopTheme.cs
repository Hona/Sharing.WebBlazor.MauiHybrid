using MudBlazor;

namespace MyShop.UI.Shared;

public class MyShopTheme : MudTheme
{
    public static readonly string[] BrandedFontFamilies =
    {
        "OpenSans",
        "Roboto",
        "Helvetica",
        "Arial",
        "sans-serif"
    };
    
    public MyShopTheme()
    {
        Typography = new Typography()
        {
            H1 = new H1()
            {
                FontFamily = BrandedFontFamilies,
            },
            H2 = new H2()
            {
                FontFamily = BrandedFontFamilies
            },
            H3 = new H3()
            {
                FontFamily = BrandedFontFamilies
            },
            H4 = new H4()
            {
                FontFamily = BrandedFontFamilies
            },
            H5 = new H5()
            {
                FontFamily = BrandedFontFamilies
            },
            H6 = new H6()
            {
                FontFamily = BrandedFontFamilies
            },
            Body1 = new Body1()
            {
                FontFamily = BrandedFontFamilies
            },
            Body2 = new Body2()
            {
                FontFamily = BrandedFontFamilies
            },
            Caption = new Caption()
            {
                FontFamily = BrandedFontFamilies
            },
            Default = new Default()
            {
                FontFamily = BrandedFontFamilies
            },
            Button = new Button()
            {
                FontFamily = BrandedFontFamilies
            },
            Overline = new Overline()
            {
                FontFamily = BrandedFontFamilies
            },
            Subtitle1 = new Subtitle1()
            {
                FontFamily = BrandedFontFamilies
            },
            Subtitle2 = new Subtitle2()
            {
                FontFamily = BrandedFontFamilies
            }
        };
    }
}