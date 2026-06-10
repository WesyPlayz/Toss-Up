using System.Buffers.Text;

namespace Toss_Up;

/// <summary>
/// 
/// </summary>
public partial class Interface : Form
{
    private const double Rate = 0.00001;
    private const int Intermission = 4000;
    private const string Prompt = "Hold down the button to toss!";
    private const string Button_Text = "Click Me!";

    private static readonly ( int X, int Y ) Start = ( 100, 320 );

    /// <summary>
    /// 
    /// </summary>
    private System.Windows.Forms.Timer Process = new ();

    /// <summary>
    /// 
    /// </summary>
    private bool Input = false;
    private int Best_Score = 0;

    /// <summary>
    /// 
    /// </summary>
    private DateTime Last_Tick = DateTime.MinValue;

    private ( int BASE, int CURRENT ) Power = default;
    private ( double BASE, double CURRENT ) Distance = default;

    /// <summary>
    /// 
    /// </summary>
    public Interface () => this.Render();

    /// <summary>
    /// 
    /// </summary>
    /// <param name = "button"></param>
    private void Initialize () 
    {
        this.Toss_Button.MouseDown += this.Charge!;
        this.Toss_Button.MouseUp += this.Toss!;
    }

    /// <summary>
    /// 
    /// </summary>
    private void Reset () 
    {
        this.Toss_Button.Location = new ( Start.X, Start.Y );

        this.Display_Prompt();

        this.Toss_Button.MouseDown += this.Charge!;
        this.Toss_Button.MouseUp += this.Toss!;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name = "score"></param>
    private void Apply_Score ( int score ) 
    {
        this.Display_Score( score );

        if ( score > this.Best_Score ) this.Best_Score = score;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name = "sender"></param>
    /// <param name = "args"></param>
    private void Charge ( object sender, EventArgs args ) 
    {
        if ( this.Input ) return;

        this.Input = true;
        this.Last_Tick = DateTime.Now;

        this.Process.Tick += this.Tick_Charge!;

        this.Process.Start();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name = "sender"></param>
    /// <param name = "args"></param>
    private void Tick_Charge ( object sender, EventArgs args ) 
    {
        if ( !this.Input ) return;

        if ( DateTime.Now - this.Last_Tick >= TimeSpan.FromSeconds( Rate ) )
        {
            this.Last_Tick = DateTime.Now;

            this.Display_Power( this.Power.BASE++ );
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name = "sender"></param>
    /// <param name = "args"></param>
    private void Toss ( object sender, EventArgs args ) 
    {
        if ( !this.Input ) return;

        this.Process.Stop();

        this.Input = false;
        this.Last_Tick = DateTime.Now;

        this.Process.Tick -= this.Tick_Charge!;
        this.Process.Tick += this.Tick_Toss!;

        this.Power.CURRENT = this.Power.BASE;

        for ( int current_Power = this.Power.BASE; current_Power > 0; current_Power-- ) 
            this.Distance.BASE += 1 + 8 * Math.Abs( ( double )( this.Power.BASE - current_Power ) / this.Power.BASE - 0.5 );

        this.Process.Start();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name = "sender"></param>
    /// <param name = "args"></param>
    private async void Tick_Toss ( object sender, EventArgs args ) 
    {
        if ( this.Input ) return;

        if ( this.Power.CURRENT == 0 )
        {
            this.Toss_Button.Location = new ( this.Toss_Button.Location.X, Start.Y );

            this.Distance = default;
            this.Power = default;

            this.Apply_Score( this.Toss_Button.Location.X );
            this.Process.Stop();

            this.Process.Tick -= this.Tick_Toss!;

            this.Toss_Button.MouseDown -= this.Charge!;
            this.Toss_Button.MouseUp -= this.Toss!;

            await Task.Delay( Intermission );
            this.Reset();

            return;
        }
        if ( DateTime.Now - this.Last_Tick >= TimeSpan.FromSeconds( Rate ) )
        {
            this.Last_Tick = DateTime.Now;

            double dx = 5 + 10 * Math.Abs( 1 - ( double )( ( this.Power.BASE - this.Power.CURRENT ) / this.Power.BASE ) - 0.5 );

            this.Distance.CURRENT += dx;

            double t = this.Distance.CURRENT / this.Distance.BASE;

            if ( t > 1 ) t = 1;

            this.Toss_Button.Location = new(
                this.Toss_Button.Location.X + ( int )dx,
                Start.Y - ( int )( -4 * this.Power.BASE * ( t - 0.5 ) * ( t - 0.5 ) + this.Power.BASE )
            );
            this.Power.CURRENT--;

            if ( t >= 1 )
            {
                this.Toss_Button.Location = new( this.Toss_Button.Location.X, Start.Y );

                this.Distance = default;
                this.Power = default;

                this.Apply_Score( this.Toss_Button.Location.X );
                this.Process.Stop();

                this.Process.Tick -= this.Tick_Toss!;

                this.Toss_Button.MouseDown -= this.Charge!;
                this.Toss_Button.MouseUp -= this.Toss!;

                await Task.Delay( Intermission );
                this.Reset();

                return;
            }
        }
    }
}
