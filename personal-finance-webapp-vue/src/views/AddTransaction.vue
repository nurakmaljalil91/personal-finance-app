<template>
  <div>
    <router-link to="/">Home</router-link>
    <TransactionForm />
  </div>
</template>

<script>
import TransactionForm from '@/components/TransactionForm'
export default {
  name: 'AddTransaction',
  components: {
    TransactionForm
  }
}
</script>

<style>
</style>
