using Xamarin.Forms;

namespace TTSSX
{
    class PassageCell : ViewCell
    {
        Label lbLine, lbDirection, lbTime, lbTramDesc;
        StackLayout mainLayout, cellWraper;

        public static BindableProperty LineProperty =
            BindableProperty.Create("Line", typeof(string), typeof(PassageCell), "00");
        public static BindableProperty DirectionProperty =
            BindableProperty.Create("Direction", typeof(string), typeof(PassageCell), "Nowhere");
        public static BindableProperty TimeProperty =
            BindableProperty.Create("Time", typeof(string), typeof(PassageCell), "Never");
        public static BindableProperty OldProperty =
            BindableProperty.Create("Old", typeof(bool), typeof(PassageCell), false);
        public static BindableProperty TramDescProperty =
            BindableProperty.Create("TramDesc", typeof(string), typeof(PassageCell), null);
        public static BindableProperty RelativeTimeProperty =
            BindableProperty.Create("RelativeTime", typeof(int), typeof(PassageCell), 0);

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

        public bool Old
        {
            get { return (bool)GetValue(OldProperty); }
            set { SetValue(OldProperty, value); }
        }

        public string TramDesc
        {
            get { return (string)GetValue(TramDescProperty); }
            set { SetValue(TramDescProperty, value); }
        }

        public int RelativeTime
        {
            get { return (int)GetValue(RelativeTimeProperty); }
            set { SetValue(RelativeTimeProperty, value); }
        }

        public PassageCell()
        {
            cellWraper = new StackLayout();
            StackLayout directionStack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                BackgroundColor = Color.Transparent,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };

            mainLayout = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal
            };

            lbLine = new Label()
            {
                WidthRequest = 30,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Start,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };

            lbDirection = new Label()
            {
                HorizontalTextAlignment = TextAlignment.Start,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };

            lbTramDesc = new Label
            {
                HorizontalTextAlignment = TextAlignment.Start,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label))
            };

            lbTime = new Label()
            {
                HorizontalOptions = LayoutOptions.End,
                VerticalTextAlignment = TextAlignment.Center
            };

            directionStack.Children.Add(lbDirection);
            directionStack.Children.Add(lbTramDesc);

            mainLayout.Children.Add(lbLine);
            mainLayout.Children.Add(directionStack);
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
                lbTime.Text = Old ? ((int)(RelativeTime / 60)).ToString() + " Min" : Time;
                lbTramDesc.Text = TramDesc;
                mainLayout.BackgroundColor = Old ? Color.Silver : Color.Transparent;
                cellWraper.BackgroundColor = Old ? Color.Silver : Color.Transparent;
            }
        }
    }
}
