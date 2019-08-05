<template>
 <span>
    <v-navigation-drawer
      v-model="drawer"
      :clipped="$vuetify.breakpoint.smAndUp"
      app
    > 
      <v-list dense>
        <v-list-item @click="">
          <v-list-item-icon>
            <v-icon>dashboard</v-icon>
          </v-list-item-icon>
          <v-list-item-content>
            <v-list-item-title>Ventas</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-navigation-drawer>
      <v-app-bar 
      :clipped-left="$vuetify.breakpoint.smAndUp"
      app 
      color="teal lighten-1" 
      dark
    >
      <v-app-bar-nav-icon @click.stop="drawer = !drawer"></v-app-bar-nav-icon>
      <v-toolbar-title>Drogueria Colombia's Manager </v-toolbar-title>
    </v-app-bar>
  </span>
</template>

<script>
export default {
  name: 'navigationDrawer',
  data() {
    return {
      drawer: null
    }
  },
};
</script>
