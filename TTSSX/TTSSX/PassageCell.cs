using Xamarin.Forms;

namespace TTSSX
{
    class PassageCell : ViewCell
    {
        Label lbLine, lbDirection, lbTime;

        public static BindableProperty LineProperty =
            BindableProperty.Create("Line", typeof(string), typeof(PassageCell), "00");
        public static BindableProperty DirectionProperty =
            BindableProperty.Create("Direction", typeof(string), typeof(PassageCell), "Nowhere");
        public static BindableProperty TimeProperty =
            BindableProperty.Create("Time", typeof(string), typeof(PassageCell), "Never");

        public string Line {
            get { return (string)GetValue(LineProperty); }
            set { SetValue(LineProperty, value); }
        }

        public string Direction
        {
            get { return (string)GetValue(DirectionProperty); }
            set { SetValue(DirectionProperty, value); }
        }

        public string Time
        {
            get { return (string)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        public PassageCell()
        {
            StackLayout cellWraper = new StackLayout();
            StackLayout mainLayout = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal
            };

            lbLine = new Label()
            {
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start,
                VerticalTextAlignment = TextAlignment.Center
            };

            lbDirection = new Label()
            {
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };
            lbTime = new Label()
            {
                HorizontalOptions = LayoutOptions.End,
                VerticalTextAlignment = TextAlignment.Center
            };

            mainLayout.Children.Add(lbLine);
            mainLayout.Children.Add(lbDirection);
            mainLayout.Children.Add(lbTime);

            cellWraper.Children.Add(mainLayout);
            View = cellWraper;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext != null)
            {
                lbLine.Text = Line;
                lbDirection.Text = Direction;
                lbTime.Text = Time;
            }
        }
    }
}
