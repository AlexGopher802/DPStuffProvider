package com.example.dpstuffproviderstore

import android.content.Context
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import androidx.fragment.app.Fragment
import com.example.dpstuffproviderstore.fragment.*
import com.example.dpstuffproviderstore.models.ClientData
import com.example.dpstuffproviderstore.other.ClientApiService
import kotlinx.android.synthetic.main.activity_main.*

class MainActivity : AppCompatActivity() {

    enum class EnumModeSearch{ CATEGORY, NAME }

    //val listCategory : List<CategoryData> = listOf(CategoryData(1, "1cat", ""), CategoryData(2, "2cat", ""), CategoryData(3, "3cat", ""))
    var productCategory : String = ""
    var productName : String = ""
    var modeSearch = EnumModeSearch.CATEGORY
    var client: ClientData? = null

    var accountFragment: Fragment? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        val homeFragment = HomeFragment()
        val catalogFragment = CatalogFragment()
        val cartFragment = CartFragment()

        val sharedPreferences = getSharedPreferences("sp", Context.MODE_PRIVATE)
        if(sharedPreferences.getBoolean("isLogin", false)){
            accountFragment = AccountFragment()

            ClientApiService().getClient(sharedPreferences.getString("login", "")!!, sharedPreferences.getString("password", "")!!){
                if (it != null){
                    client = it
                }
                else{
                    Log.i("myLog", "ХуеТА")
                }
            }
        }
        else{
            accountFragment = AccountNotLoginFragment()
        }

        setContentView(R.layout.activity_main)

        makeCurrentFragment(homeFragment)

        bottomNavMenu.setOnNavigationItemSelectedListener {
            when(it.itemId){
                R.id.home -> makeCurrentFragment(homeFragment)
                R.id.catalog -> makeCurrentFragment(catalogFragment)
                R.id.cart -> makeCurrentFragment(cartFragment)
                R.id.account -> makeCurrentFragment(accountFragment!!)
            }
            true
        }


    }

    public fun makeCurrentFragment(fragment: Fragment) =
        supportFragmentManager.beginTransaction().apply {
            replace(R.id.frameWrapper, fragment, "activeFragment")
            commit()
        }


}