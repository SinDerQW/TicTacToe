package com.example.tictactoe


import android.os.Bundle
import android.widget.Button
import androidx.activity.ComponentActivity
import androidx.activity.compose.setContent
import androidx.activity.enableEdgeToEdge
import androidx.compose.foundation.background
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Spacer
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.layout.size
import androidx.compose.material3.Button
import androidx.compose.material3.Scaffold
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.graphics.Shape
import androidx.compose.ui.modifier.modifierLocalOf
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.unit.dp
import com.example.tictactoe.ui.theme.TicTacToeTheme

class MainActivity : ComponentActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        enableEdgeToEdge()
        setContent {
            TicTacToeTheme {
                Greeting()
            }
        }
    }
}

@Preview(showSystemUi = true)
@Composable
fun Greeting( ) {
        Column(
            Modifier.fillMaxSize().padding(8.dp),
            verticalArrangement =  Arrangement.Center,
            horizontalAlignment =  Alignment.CenterHorizontally
        ) {
            Text( text =  "Крестики х Нолики" )

            Spacer(Modifier.padding(32.dp))

            Button(onClick = {

            }) {
                Text("Играть!")
            }
            Spacer(Modifier.padding(8.dp))
            Button(onClick = {

            }) {
                Text("Настройки!")
            }
        }
}


