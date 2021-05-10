package com.example.dpstuffproviderstore

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import androidx.fragment.app.Fragment
import androidx.fragment.app.FragmentManager
import com.example.dpstuffproviderstore.fragment.AccountFragment
import com.example.dpstuffproviderstore.fragment.CartFragment
import com.example.dpstuffproviderstore.fragment.CatalogFragment
import com.example.dpstuffproviderstore.fragment.HomeFragment
import com.example.dpstuffproviderstore.models.CategoryData
import kotlinx.android.synthetic.main.activity_main.*

class MainActivity : AppCompatActivity() {

    enum class EnumModeSearch{ CATEGORY, NAME }

    //val listCategory : List<CategoryData> = listOf(CategoryData(1, "1cat", ""), CategoryData(2, "2cat", ""), CategoryData(3, "3cat", ""))
    var productCategory : String = ""
    var productName : String = ""
    var modeSearch = EnumModeSearch.CATEGORY

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        val homeFragment = HomeFragment()
        val catalogFragment = CatalogFragment()
        val cartFragment = CartFragment()
        val accountFragment = AccountFragment()

        makeCurrentFragment(homeFragment)

        bottomNavMenu.setOnNavigationItemSelectedListener {
            when(it.itemId){
                R.id.home -> makeCurrentFragment(homeFragment)
                R.id.catalog -> makeCurrentFragment(catalogFragment)
                R.id.cart -> makeCurrentFragment(cartFragment)
                R.id.account -> makeCurrentFragment(accountFragment)
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