package com.example.dpstuffprovider

import android.content.Context
import android.content.Intent
import android.content.SharedPreferences
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.view.View
import android.widget.Toast
import com.example.dpstuffprovider.models.CouriersData
import kotlinx.android.synthetic.main.activity_main.*
import retrofit2.*
import retrofit2.converter.gson.GsonConverterFactory


class MainActivity : AppCompatActivity() {

    val retrofit = Retrofit.Builder()
        .baseUrl("https://dpspapiv220210407004655.azurewebsites.net/api/")
        .addConverterFactory(GsonConverterFactory.create())
        .build()
    val api = retrofit.create(ICouriers::class.java)

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

        val call = api.GetCouriers(login, password)
        call.enqueue(object : Callback<List<CouriersData>> {
            override fun onResponse(call: Call<List<CouriersData>>,
                                    response: Response<List<CouriersData>>) {
                if(response.code() == 200){
                    val courier = response.body()!![0]

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
                    Toast.makeText(applicationContext, "Error-code: ${response.code()}", Toast.LENGTH_LONG).show()
                }
            }

            override fun onFailure(call: Call<List<CouriersData>>, t: Throwable){
                Toast.makeText(applicationContext, t.message, Toast.LENGTH_LONG).show()
            }
        })
    }

    fun onClickLogin(view : View){
        login(etLogin.text.toString(), etPassword.text.toString())
    }
}
