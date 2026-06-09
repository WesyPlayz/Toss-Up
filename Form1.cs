namespace Toss_Up;

/// <summary>
/// 
/// </summary>
public partial class Interface : Form
{
    private const double Rate = 0.0001;

    /// <summary>
    /// 
    /// </summary>
    private System.Windows.Forms.Timer Process = new ();

    /// <summary>
    /// 
    /// </summary>
    private bool Input = false;

    /// <summary>
    /// 
    /// </summary>
    private DateTime Last_Tick = DateTime.MinValue;

    private int Power = 0;
    private int Current_Power = 0;
    private int Distance = 0;
    private int Current_Traveled = 0;

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
            this.Toss_Button.Text = $"{ this.Power++ }";
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

        this.Process.Start();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name = "sender"></param>
    /// <param name = "args"></param>
    private void Tick_Toss ( object sender, EventArgs args ) 
    {

    }
}
