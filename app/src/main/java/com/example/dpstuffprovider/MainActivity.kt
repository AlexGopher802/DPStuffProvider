package com.example.dpstuffprovider

import android.content.Context
import android.content.Intent
import android.content.SharedPreferences
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import android.widget.Toast
import com.example.dpstuffprovider.api.ApiService
import com.example.dpstuffprovider.interfaces.ICouriers
import com.example.dpstuffprovider.models.CouriersData
import kotlinx.android.synthetic.main.activity_main.*
import retrofit2.*
import retrofit2.converter.gson.GsonConverterFactory

/**
 * Стартовое активити с формой авторизации
 */
class MainActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        val sp : SharedPreferences = getSharedPreferences("sp", Context.MODE_PRIVATE)
        if(sp.getBoolean("isLogin", false)){
            login(sp.getString("login", "")!!, sp.getString("password", "")!!)
        }
    }

    override fun onRestart() {
        super.onRestart()
        progressBarOff()
        getSharedPreferences("sp", Context.MODE_PRIVATE).edit().putBoolean("isLogin", false).apply()
    }

    fun login(login : String, password : String){
        progressBarOn()

        ApiService().getCouriers(login, password) {
            if(it != null){
                val courier = it[0]

                Toast.makeText(applicationContext, "Здравствуйте, ${courier.firstName}", Toast.LENGTH_LONG).show()

                val editor : SharedPreferences.Editor = getSharedPreferences("sp", Context.MODE_PRIVATE).edit()
                editor.putBoolean("isLogin", true)
                editor.putString("login", login)
                editor.putString("password", password)
                editor.apply()

                val intent : Intent = Intent(applicationContext, MainMenu::class.java)
                intent.putExtra("courier", courier)
                startActivity(intent)
            }
            else{
                progressBarOff()
                Toast.makeText(applicationContext, "Неверный логин и/или пароль", Toast.LENGTH_LONG).show()
            }
        }
    }

    fun onClickLogin(view : View){
        login(etLogin.text.toString(), etPassword.text.toString())
    }

    fun progressBarOn(){
        progressBar.visibility = View.VISIBLE
        btnLogin.visibility = View.GONE
    }

    fun progressBarOff(){
        progressBar.visibility = View.GONE
        btnLogin.visibility = View.VISIBLE
    }
}
