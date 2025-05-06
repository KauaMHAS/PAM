using System;
using Microsoft.Maui.Controls;

namespace DiceRoller_Kaua_Julia
{
    public partial class MainPage : ContentPage
    {
        readonly string[] diceImages = {
            "dado1.png","dado2.png","dado3.png",
            "dado4.png","dado5.png","dado6.png"
        };

        readonly Dice dice = new Dice();
        readonly Game game = new Game();
        int somaTotal = 0;
        readonly Random rand = new Random();

        public MainPage()
        {
            InitializeComponent();

            DiceImage.IsVisible = false;

            DiceTypePicker.SelectedIndexChanged += (s, e) =>
            {
                var tipo = DiceTypePicker.SelectedItem as string;
                if (tipo == null) return;

                int faces = int.Parse(tipo.Substring(1));
                NumberPicker.Items.Clear();
                for (int i = 1; i <= faces; i++)
                    NumberPicker.Items.Add(i.ToString());

                DiceImage.IsVisible = (faces == 6);
                if (faces == 6)
                    DiceImage.Source = diceImages[0];
            };
        }

        private async void OnRollButtonClicked(object sender, EventArgs e)
        {
            if (DiceTypePicker.SelectedIndex < 0 || NumberPicker.SelectedIndex < 0)
            {
                await DisplayAlert("Aviso",
                    "Escolha primeiro o tipo de dado e depois um número.",
                    "OK");
                return;
            }

            int faces = int.Parse(
                (DiceTypePicker.SelectedItem as string).Substring(1)
            );

            if (somaTotal >= 25)
            {
                await DisplayAlert("Fim de Jogo",
                    "Tentativas máximas utilizadas.\n" +
                    "A soma dos lados opostos chegou a 25.",
                    "OK");
                return;
            }

            int resultado = faces == 6
                ? dice.Roll()
                : rand.Next(1, faces + 1);

            int oposto = (faces + 1) - resultado;
            somaTotal += oposto;

            if (faces == 6)
                DiceImage.Source = diceImages[resultado - 1];

            int escolhido = int.Parse(NumberPicker.SelectedItem.ToString());
            bool acertou = game.CheckWinner(escolhido, resultado);

            SumLabel.Text = $"Soma dos lados opostos: {somaTotal}";
            VictoriesLabel.Text = $"Total de vitórias: {game.PlayerPoint}";
            StreakLabel.Text = $"Sequência de vitórias: {game.Streak}";

            await DisplayAlert(acertou ? "Parabéns!" : "Ops!",
                acertou
                  ? "Você acertou!"
                  : $"Você errou. O dado caiu em {resultado}.",
                "OK");

            if (somaTotal >= 25)
                await DisplayAlert("Fim de Jogo",
                    "Tentativas máximas utilizadas.\n" +
                    "A soma dos lados opostos chegou a 25.",
                    "OK");
        }
    }
}