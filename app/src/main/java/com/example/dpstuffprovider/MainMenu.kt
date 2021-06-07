package com.example.dpstuffprovider

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import androidx.fragment.app.Fragment
import com.example.dpstuffprovider.fragments.AccountFragment
import com.example.dpstuffprovider.fragments.ActiveDeliveryFragment
import com.example.dpstuffprovider.fragments.AllDeliveryFragment
import com.example.dpstuffprovider.models.CouriersData
import kotlinx.android.synthetic.main.activity_main_menu.*

/**
 * Основное активити с навигационным меню
 */
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
