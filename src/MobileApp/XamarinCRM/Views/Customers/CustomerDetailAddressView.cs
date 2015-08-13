﻿using Xamarin.Forms;
using XamarinCRM.Interfaces;
using XamarinCRM.Layouts;
using XamarinCRM.Pages.Customers;
using XamarinCRM.Statics;
using XamarinCRM.ViewModels.Customers;
using XamarinCRM.Views.Base;

namespace XamarinCRM
{
    public class CustomerDetailAddressView : ModelTypedContentView<CustomerDetailViewModel>
    {
        readonly INativeDirectionsPresenter _NativeMap;

        public CustomerDetailAddressView()
        {
            _NativeMap = DependencyService.Get<INativeDirectionsPresenter>();

            StackLayout stackLayout = new UnspacedStackLayout() { Padding = new Thickness(20) };

            Label addressTitleLabel = new Label()
            { 
                Text = TextResources.Customers_Detail_Address,
                TextColor = Device.OnPlatform(Palette._007, Palette._009, Palette._008),
                FontSize = Device.OnPlatform(Device.GetNamedSize(NamedSize.Small, typeof(Label)), Device.GetNamedSize(NamedSize.Small, typeof(Label)), Device.GetNamedSize(NamedSize.Small, typeof(Label))),
                LineBreakMode = LineBreakMode.TailTruncation
            };

            Label addressStreetLabel = new Label()
            { 
                TextColor = Palette._008, 
                FontSize = Device.OnPlatform(Device.GetNamedSize(NamedSize.Default, typeof(Label)), Device.GetNamedSize(NamedSize.Medium, typeof(Label)), Device.GetNamedSize(NamedSize.Default, typeof(Label))),
                LineBreakMode = LineBreakMode.TailTruncation
            };
            addressStreetLabel.SetBinding(Label.TextProperty, "Account.Street");

            Label addressCityLabel = new Label()
            { 
                TextColor = Palette._008, 
                FontSize = Device.OnPlatform(Device.GetNamedSize(NamedSize.Default, typeof(Label)), Device.GetNamedSize(NamedSize.Medium, typeof(Label)), Device.GetNamedSize(NamedSize.Default, typeof(Label))),
                LineBreakMode = LineBreakMode.TailTruncation
            };
            addressCityLabel.SetBinding(Label.TextProperty, "Account.City");

            Label addressStatePostalLabel = new Label()
            { 
                TextColor = Palette._008, 
                FontSize = Device.OnPlatform(Device.GetNamedSize(NamedSize.Default, typeof(Label)), Device.GetNamedSize(NamedSize.Medium, typeof(Label)), Device.GetNamedSize(NamedSize.Default, typeof(Label))),
                LineBreakMode = LineBreakMode.TailTruncation
            };
            addressStatePostalLabel.SetBinding(Label.TextProperty, "Account.StatePostal");

            Image mapMarkerImage = new Image()
            { 
                Source = new FileImageSource { File = Device.OnPlatform("map_marker_ios", "map_marker_android", null) }, 
                Aspect = Aspect.AspectFit, 
                HeightRequest = 25 
            }; 
            mapMarkerImage.GestureRecognizers.Add(
                new TapGestureRecognizer()
                { 
                    Command = new Command(async () =>
                        {
                            NavigationPage navPage = new NavigationPage(new CustomerMapPage(ViewModel));
                            ViewModel.PushModalAsync(navPage);
                        }) 
                });

            stackLayout.Children.Add(addressTitleLabel);
            stackLayout.Children.Add(addressStreetLabel);
            stackLayout.Children.Add(addressCityLabel);
            stackLayout.Children.Add(addressStatePostalLabel);

            AbsoluteLayout absoluteLayout = new AbsoluteLayout();

            absoluteLayout.Children.Add(stackLayout, new Rectangle(0, .5, 1, AbsoluteLayout.AutoSize), AbsoluteLayoutFlags.PositionProportional | AbsoluteLayoutFlags.WidthProportional);

            absoluteLayout.Children.Add(mapMarkerImage, new Rectangle(.9, .5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize), AbsoluteLayoutFlags.PositionProportional);

            Content = absoluteLayout;
        }
    }
}

