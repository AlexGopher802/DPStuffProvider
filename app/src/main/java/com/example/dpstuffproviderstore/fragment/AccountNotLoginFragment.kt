package com.example.dpstuffproviderstore.fragment

import android.os.Bundle
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import com.example.dpstuffproviderstore.MainActivity
import com.example.dpstuffproviderstore.R
import kotlinx.android.synthetic.main.fragment_account_notlogin.view.*

/**
 * Фрагмент не залогиненного пользователя
 */
class AccountNotLoginFragment : Fragment() {

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        var inflate = inflater.inflate(R.layout.fragment_account_notlogin, container, false)

        inflate.btnLogin.setOnClickListener {
            val mainActivity = activity as MainActivity
            mainActivity.makeCurrentFragment(AccountLoginFragment())
        }

        return inflate
    }

}