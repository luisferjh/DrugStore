<template>
  <v-card>
    <v-card-title>
      <span class="headline">Nuevo Proveedor</span>
    </v-card-title>
    <v-card-text>
      <v-container>
        <v-row>
          <v-col cols="12" sm="6" md="12">
            <v-text-field label="Nombre del Proveedor*" v-model="providerObj.providerName" required></v-text-field>
          </v-col>
              
          <v-col cols="12" sm="6" md="12">
            <v-text-field label="Numero de Documento" v-model="providerObj.documentNumber" required></v-text-field>
          </v-col>
                  
            <v-col cols="12">
            <v-text-field label="Direccion" v-model="providerObj.address" required></v-text-field>
          </v-col>         

          <v-col cols="12">
            <v-text-field label="Numero de telefono" v-model="providerObj.phoneNumber" required></v-text-field>
          </v-col>

          <v-col cols="12">
            <v-text-field label="Email" v-model="providerObj.email" required></v-text-field>
          </v-col>
        </v-row>
      </v-container>
      <small>*indica campo obligatorio</small>
      
    </v-card-text>
    <v-card-actions>
      <div class="flex-grow-1"></div>
      <v-btn color="blue darken-1" text @click="cancel()">Cerrar</v-btn>
      <v-btn color="blue darken-1" text @click="save()">Guardar</v-btn>
    </v-card-actions>
  </v-card> 
</template>

<script>
  export default {
    props:{
      provider:{
        type:Object,
        required:true
      },
    },
    data() {
      return {
       providerObj: Object.assign({}, this.provider) 
      }
    },
    watch: {
     //este watcher nos ayuda a actualizar 
     //el producto enviado desde el componente padre
      provider(newValue, oldValue) {        
        this.providerObj = Object.assign({}, newValue)
      }
    },
    methods: {
      save() {
        this.$emit('save',{provider:this.providerObj})
      },
      cancel(){
        this.$emit('cancel',false)
      }
    },
  }
</script>

