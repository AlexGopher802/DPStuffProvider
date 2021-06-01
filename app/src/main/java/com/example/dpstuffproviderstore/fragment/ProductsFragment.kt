package com.example.dpstuffproviderstore.fragment

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.dpstuffproviderstore.MainActivity
import com.example.dpstuffproviderstore.R
import com.example.dpstuffproviderstore.`interface`.ICategory
import com.example.dpstuffproviderstore.`interface`.IProduct
import com.example.dpstuffproviderstore.adapter.CategoryAdapter
import com.example.dpstuffproviderstore.adapter.ProductAdapter
import com.example.dpstuffproviderstore.models.CategoryData
import com.example.dpstuffproviderstore.models.ProductData
import com.example.dpstuffproviderstore.other.ClientApiService
import kotlinx.android.synthetic.main.fragment_catalog.view.*
import kotlinx.android.synthetic.main.fragment_products.view.*
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

/**
 * Фрагмент со списков товаров (по наименованию или категории)
 */
class ProductsFragment : Fragment() {

    var inflate : View? = null

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?,
                              savedInstanceState: Bundle?): View? {
        inflate = inflater.inflate(R.layout.fragment_products, container, false)
        val mainActivity = activity as MainActivity

        inflate!!.recyclerProducts.layoutManager = LinearLayoutManager(context!!)

        if(mainActivity.modeSearch == MainActivity.EnumModeSearch.CATEGORY)
        {
            ClientApiService().getProductsByCategory(mainActivity.productCategory) {

                if(it != null){
                    inflate!!.recyclerProducts.adapter = ProductAdapter(it, this@ProductsFragment)
                }
                else{
                    val fragment = ErrorFragment()
                    fragment.statusCode = "404"
                    mainActivity.makeCurrentFragment(fragment)
                }
            }
        }

        if(mainActivity.modeSearch == MainActivity.EnumModeSearch.NAME)
        {
            ClientApiService().getProductsByName(mainActivity.productName) {

                if(it != null){
                    inflate!!.recyclerProducts.adapter = ProductAdapter(it, this@ProductsFragment)
                }
                else{
                    val fragment = ErrorFragment()
                    fragment.statusCode = "404"
                    mainActivity.makeCurrentFragment(fragment)
                }
            }
        }

        return inflate
    }

}