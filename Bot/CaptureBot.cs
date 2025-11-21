using LetterStomach.Bot.Interface;
using LetterStomach.Models;
using System.Text.RegularExpressions;
using Capture = LetterStomach.Models.Capture;

namespace LetterStomach.Bot
{
    public class CaptureBot : ICaptureBot
    {
        #region ERROR
        private bool _error_on = true;
        private bool _error_off = false;
        private string _error_message;

        public string error_message
        {
            get => this._error_message;
            set
            {
                this._error_message = value;
            }
        }

        public event EventHandler<string> OnError;
        #endregion

        #region VARIABLE
        private bool is_insert = false;
        private const string DARK = "dark";
        private const string LIGHT = "light";
        private const string FRONT = "front";
        private const string REAR = "rear";

        #endregion

        #region BUTTON
        public async Task<string[]> Picker()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation picker \"Capture\" bot failed!");
                FileResult pick_result = await FilePicker.Default.PickAsync();
                string file_name = pick_result.FileName;
                Stream file_stream = await pick_result.OpenReadAsync();
                string value = string.Empty;
                StreamReader reader = new StreamReader(file_stream);
                string conteudo = await reader.ReadToEndAsync();
                value = conteudo;
                string[] lines = value.Split('\n');
                return lines;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return null;
            }
        }

        public async Task<List<Capture>> ReadCapture(string[] lines)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation read \"Capture\" bot failed!");

                List<Capture> captures = new List<Capture>();
                int quantity = 0;
                foreach (string line in lines)
                {
                    quantity++;
                    if (quantity == 1) continue;
                    if (line == string.Empty) break;

                    string terms = line.Replace("\r", "");
                    string[] parts = terms.Split(';');
                    captures.Add(new Capture
                    {
                        environment = parts[0],
                        rotate = parts[1],
                        flash = parts[2],
                        result = parts[3].ToUpper() == "TRUE"? true : false,
                    });
                }
                return captures;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return null;
            }
        }

        public int GetNode(List<Capture> captures)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation get node \"Capture\" bot failed!");
                return 3;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return -1;
            }
        }

        public async Task<object> Load()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation load \"Capture\" bot failed!");

                DecisionTree decision_tree = new DecisionTree();
                string[] picker = await Picker();
                List<Capture> captures = new List<Capture>();
                captures = await ReadCapture(picker);
                int node = GetNode(captures);
                for (int quantity = 1; quantity <= node; quantity++)
                {
                    DecisionTree node_tree = new DecisionTree();
                    this.is_insert = false;
                    node_tree = Mount();
                    if (node_tree != null) decision_tree = node_tree;
                }

                return decision_tree;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return null;
            }
        }

        public DecisionTree Mount()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation picker command \"Bot\" view model failed!");
                return null;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return null;
            }
        }

        public object Rule()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation picker command \"Bot\" view model failed!");
                return null;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return null;
            }
        }

        public object Write()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation picker command \"Bot\" view model failed!");
                return null;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return null;
            }
        }
        #endregion

        #region DECICION
        public object MountRoot(List<Capture> captures)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount root \"Capture\" bot failed!");

                //-----
                List<bool> result = captures.Select(index => index.result).ToList();
                double result_true = result.FindAll(index => index.Equals(true)).Count();
                double result_false = result.FindAll(index => index.Equals(false)).Count();
                double result_all = result.Count();

                double formula = -((result_true / result_all) * (Math.Log2(result_true / result_all)));
                formula -= ((result_false / result_all) * (Math.Log2(result_false / result_all)));
                if (double.IsNaN(formula)) formula = 0;

                double profit_environment = MountEnvironment(captures, formula);


                return null;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return null;
            }
        }

        public double MountEnvironment(List<Capture> captures, double formula)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount environment \"Capture\" bot failed!");

                List<bool> environment_true = captures.FindAll(index => index.environment == LIGHT).Select(index => index.result).ToList();
                double environment_true_light = environment_true.FindAll(index => index.Equals(true)).Count();
                double environment_true_dark = environment_true.FindAll(index => index.Equals(false)).Count();
                int quantity_true = 0;
                quantity_true = environment_true.Count();

                double formula_environment_true = -((environment_true_light / quantity_true) * (Math.Log2(environment_true_light / quantity_true)));
                formula_environment_true -= ((environment_true_dark / quantity_true) * (Math.Log2(environment_true_dark / quantity_true)));
                if (double.IsNaN(formula_environment_true)) formula_environment_true = 0;

                List<bool> environment_false = captures.FindAll(index => index.environment == DARK).Select(index => index.result).ToList();
                double environment_false_light = environment_true.FindAll(index => index.Equals(true)).Count();
                double environment_false_dark = environment_true.FindAll(index => index.Equals(false)).Count();
                int quantity_false = 0;
                quantity_false = environment_false.Count();

                double formular_environment_false = -((environment_false_light / quantity_false) * (Math.Log2(environment_false_light / quantity_false)));
                formular_environment_false -= ((environment_false_dark / quantity_false) * (Math.Log2(environment_false_dark / quantity_false)));
                if (double.IsNaN(formular_environment_false)) formular_environment_false = 0;

                double mount_environment_true = (((quantity_true) / (quantity_true + quantity_false)) * formula_environment_true);
                if (double.IsNaN(mount_environment_true)) mount_environment_true = 0;
                double mount_environment_false = (((quantity_false) / (quantity_true + quantity_false)) * formular_environment_false);
                if (double.IsNaN(mount_environment_false)) mount_environment_false = 0;
                double profit_environment = formula - mount_environment_true - mount_environment_false;
                if (double.IsNaN(profit_environment)) profit_environment = 0;

                return profit_environment;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return -1;
            }
        }

        public double MountRotate(List<Capture> captures)
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation mount rotate \"Capture\" bot failed!");

                List<bool> rotate_true = captures.FindAll(index => index.rotate == FRONT).Select(index => index.result).ToList();
                double rotate_true_front = rotate_true.FindAll(index => index.Equals(true)).Count();
                double rotate_true_rear = rotate_true.FindAll(index => index.Equals(false)).Count();
                int quantity_true = 0;
                quantity_true = rotate_true.Count();

                double formula_rotate_true = -((rotate_true_front / quantity_true) * (Math.Log2(rotate_true_front / quantity_true)));
                formula_rotate_true -= ((rotate_true_rear / quantity_true) * (Math.Log2(rotate_true_rear / quantity_true)));
                if (double.IsNaN(formula_rotate_true)) formula_rotate_true = 0;


                return -1;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return -1;
            }
        }

        public object MountFlash()
        {
            try
            {
                if (this._error_off) throw new InvalidOperationException("Operation picker command \"Bot\" view model failed!");
                return null;
            }
            catch (Exception ex)
            {
                this.error_message = ex.Message;
                this.OnError?.Invoke(this, this.error_message);
                return null;
            }
        }
        #endregion
    }
}
