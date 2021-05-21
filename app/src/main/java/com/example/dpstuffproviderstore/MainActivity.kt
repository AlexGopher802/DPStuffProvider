package com.example.dpstuffproviderstore

import android.content.Context
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import androidx.fragment.app.Fragment
import com.example.dpstuffproviderstore.fragment.*
import com.example.dpstuffproviderstore.models.ClientData
import com.example.dpstuffproviderstore.models.ProductData
import com.example.dpstuffproviderstore.other.ClientApiService
import com.google.gson.Gson
import com.google.gson.reflect.TypeToken
import kotlinx.android.synthetic.main.activity_main.*
import java.lang.reflect.Type
import kotlin.reflect.typeOf

class MainActivity : AppCompatActivity() {

    enum class EnumModeSearch{ CATEGORY, NAME }

    //val listCategory : List<CategoryData> = listOf(CategoryData(1, "1cat", ""), CategoryData(2, "2cat", ""), CategoryData(3, "3cat", ""))
    var productCategory : String = ""
    var productName : String = ""
    var modeSearch = EnumModeSearch.CATEGORY
    var client: ClientData? = null
    var cartList: ArrayList<ProductData>? = null

    var accountFragment: Fragment = AccountNotLoginFragment()
    var homeFragment: Fragment = HomeFragment()
    var catalogFragment: Fragment = CatalogFragment()
    var cartFragment: Fragment = CartEmptyFragment()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        val sharedPreferences = getSharedPreferences("sp", Context.MODE_PRIVATE)
        if(sharedPreferences.getBoolean("isLogin", false)){
            ClientApiService().getClient(sharedPreferences.getString("login", "")!!, sharedPreferences.getString("password", "")!!){
                if (it != null){
                    client = it
                    accountFragment = AccountFragment()
                }
                else{
                    accountFragment = AccountNotLoginFragment()
                }
            }
        }

        var jsonString: String? = sharedPreferences.getString("cartList", "")
        if(!jsonString.isNullOrEmpty()){
            cartList = Gson().fromJson(jsonString, Array<ProductData>::class.java) as ArrayList<ProductData>
            cartFragment = CartEmptyFragment()
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