package com.example.dpstuffproviderstore.fragment

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.dpstuffproviderstore.MainActivity
import com.example.dpstuffproviderstore.R
import com.example.dpstuffproviderstore.adapter.ProductAdapter
import kotlinx.android.synthetic.main.fragment_cart.view.*
import kotlinx.android.synthetic.main.fragment_products.view.*

class CartFragment : Fragment() {

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?,
                              savedInstanceState: Bundle?): View? {
        val inflate = inflater.inflate(R.layout.fragment_cart, container, false)

        inflate.recyclerCart.layoutManager = LinearLayoutManager(context!!)

        val mainActivity = activity as MainActivity

        inflate.recyclerProducts.adapter = ProductAdapter(mainActivity.cartList!!, this@CartFragment)

        return inflate
    }

}