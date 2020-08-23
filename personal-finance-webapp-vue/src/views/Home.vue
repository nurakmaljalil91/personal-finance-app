<template>
  <div>
    <h1>Personal Finance</h1>
    <Total />
    <NewAccount />
    <TransactionForm />
  </div>
</template>

<script>
import Total from '@/components/Total'
import TransactionForm from '@/components/TransactionForm'
import NewAccount from '@/components/NewAccount'
export default {
  name: 'Home',
  components: {
    Total,
    TransactionForm,
    NewAccount
  }
}
</script>

<style scoped>
</style>
