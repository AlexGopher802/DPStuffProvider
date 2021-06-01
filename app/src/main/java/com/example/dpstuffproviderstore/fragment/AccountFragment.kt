package com.example.dpstuffproviderstore.fragment

import android.content.Context
import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import com.example.dpstuffproviderstore.MainActivity
import com.example.dpstuffproviderstore.R
import kotlinx.android.synthetic.main.fragment_account.view.*

/**
 * Фрагмент аккаунта залогиненного пользователя
 */
class AccountFragment : Fragment() {

    var testText: String? = null

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?,
                              savedInstanceState: Bundle?): View? {
        val mainActivity = activity as MainActivity
        val inflate = inflater.inflate(R.layout.fragment_account, container, false)

        inflate.tvTestName.text = "${mainActivity.client!!.lastName} ${mainActivity.client!!.firstName} ${mainActivity.client!!.patronymic}"

        inflate.btnOut.setOnClickListener {
            val editor = mainActivity.getSharedPreferences("sp", Context.MODE_PRIVATE).edit()
            editor.putBoolean("isLogin", false)
            editor.apply()

            mainActivity.accountFragment = AccountNotLoginFragment()
            mainActivity.client = null
            mainActivity.makeCurrentFragment(AccountNotLoginFragment())
        }

        inflate.cardOrders.setOnClickListener {
            mainActivity.makeCurrentFragment(OrdersFragment())
        }

        return inflate
    }

}