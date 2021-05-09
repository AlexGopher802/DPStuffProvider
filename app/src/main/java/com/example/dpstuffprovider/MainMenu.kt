package com.example.dpstuffprovider

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Toast
import androidx.fragment.app.Fragment
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.example.dpstuffprovider.fragment.AccountFragment
import com.example.dpstuffprovider.fragment.ActiveDeliveryFragment
import com.example.dpstuffprovider.fragment.AllDeliveryFragment
import com.example.dpstuffprovider.models.CouriersData
import com.example.dpstuffprovider.models.OrdersData
import kotlinx.android.synthetic.main.activity_main_menu.*
import kotlinx.android.synthetic.main.fragment_account.*
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

class MainMenu : AppCompatActivity() {

    var courier : CouriersData? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main_menu)

        val allDeliveryFragment = AllDeliveryFragment()
        val activeDeliveryFragment = ActiveDeliveryFragment()
        val accountFragment = AccountFragment()

        makeCurrentFragment(allDeliveryFragment)

        bottomNavMenu.setOnNavigationItemSelectedListener {
            when(it.itemId){
                R.id.allDelivery -> makeCurrentFragment(allDeliveryFragment)
                R.id.activeDelivery -> makeCurrentFragment(activeDeliveryFragment)
                R.id.account -> makeCurrentFragment(accountFragment)
            }
            true
        }

        courier = intent.getSerializableExtra("courier") as CouriersData?
        //nameCourier.text = "${courier.lastName} ${courier.firstName} ${courier.patronymic}"
    }

    fun makeCurrentFragment(fragment: Fragment) =
        supportFragmentManager.beginTransaction().apply {
            replace(R.id.frameWrapper, fragment)
            commit()
        }
}
