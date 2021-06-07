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

        val sp : SharedPreferences = getSharedPreferences("sp", Context.MODE_PRIVATE)
        if(sp.getBoolean("isLogin", false)){
            login(sp.getString("login", "")!!, sp.getString("password", "")!!)
        }
        else{
            setContentView(R.layout.activity_main)
        }
    }

    fun validForm(): Boolean{
        return true
    }

    fun login(login : String, password : String){
        //if (!validForm()) return

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
                //Обработка ошибки
            }
        }
    }

    fun onClickLogin(view : View){
        login(etLogin.text.toString(), etPassword.text.toString())
    }
}
