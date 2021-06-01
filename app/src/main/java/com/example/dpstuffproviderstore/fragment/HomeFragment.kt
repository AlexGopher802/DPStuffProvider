package com.example.dpstuffproviderstore.fragment

import android.os.Bundle
import android.util.Log
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.dpstuffproviderstore.MainActivity
import com.example.dpstuffproviderstore.R
import com.example.dpstuffproviderstore.R.string
import com.example.dpstuffproviderstore.`interface`.IProduct
import com.example.dpstuffproviderstore.adapter.ProductAdapter
import com.example.dpstuffproviderstore.models.ProductData
import com.example.dpstuffproviderstore.other.ClientApiService
import kotlinx.android.synthetic.main.fragment_home.view.*
import kotlinx.android.synthetic.main.fragment_products.view.*
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

/**
 * Фрагмент домашней страницы со списком популярных товаров (пока просто выводит все товары)
 */
class HomeFragment : Fragment() {

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {

        val inflate = inflater.inflate(R.layout.fragment_home, container, false)
        val mainActivity = activity as MainActivity

        inflate.recyclerBestProducts.layoutManager = LinearLayoutManager(context!!)

        ClientApiService().getAllProducts() {

            if(it != null){
                inflate.recyclerBestProducts.adapter = ProductAdapter(it, this@HomeFragment)
            }
            else{
                val fragment = ErrorFragment()
                fragment.statusCode = "404"
                mainActivity.makeCurrentFragment(fragment)
            }
        }

        return inflate
    }

}