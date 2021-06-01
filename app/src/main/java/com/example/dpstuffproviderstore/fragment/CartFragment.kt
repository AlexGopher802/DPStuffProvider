package com.example.dpstuffproviderstore.fragment

import android.content.Context
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
import com.example.dpstuffproviderstore.adapter.CartAdapter
import com.example.dpstuffproviderstore.adapter.ProductAdapter
import kotlinx.android.synthetic.main.fragment_cart.view.*
import kotlinx.android.synthetic.main.fragment_products.view.*

/**
 * Фрагмент корзины, корзина заполняется по списку товаров cartList из MainActivity
 */
class CartFragment : Fragment() {

    var inflate : View? = null
    var summ : Double = 0.0

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?,
                              savedInstanceState: Bundle?): View? {
        inflate = inflater.inflate(R.layout.fragment_cart, container, false)
        val mainActivity = activity as MainActivity

        inflate!!.recyclerCart.layoutManager = LinearLayoutManager(context!!)
        inflate!!.recyclerCart.adapter = CartAdapter(mainActivity.cartList, this@CartFragment)

        inflate!!.tvTitle.text = "${mainActivity.cartList.size} Товара:"

        summ = 0.0
        for(i in mainActivity.cartList){
            summ += i.price!! * (i.quantity ?: 1)
        }
        inflate!!.tvCheckout.text = "Итого: ${summ} ₽"

        inflate!!.btnCheckout.setOnClickListener {
            if(mainActivity.getSharedPreferences("sp", Context.MODE_PRIVATE).getBoolean("isLogin", false)){
                val fragment = CheckoutFragment()
                fragment.checkSumm = summ
                mainActivity.makeCurrentFragment(fragment)
            }
            else{
                Toast.makeText(requireContext(), "Вы не авторизованы", Toast.LENGTH_LONG).show()
            }
        }

        return inflate
    }
}