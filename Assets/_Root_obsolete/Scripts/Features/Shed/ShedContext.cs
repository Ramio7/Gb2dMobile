using Features.Inventory;
using Features.Shed.Upgrade;
using System;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Features.Shed
{
    internal class ShedContext : BaseContext
    {
        private static readonly ResourcePath _viewPath = new("Prefabs/Shed/ShedView");
        private static readonly ResourcePath _dataSourcePath = new("Configs/Shed/UpgradeItemConfigDataSource");

        public UpgradeHandlersRepository UpgradeHandlersRepository { get; private set; }
        public ShedView ShedView { get; private set; }
        public InventoryContext InventoryContext { get; private set; }

        public ShedContext(Transform placeForUi, IInventoryModel model)
        {
            if (placeForUi == null)
                throw new ArgumentNullException(nameof(placeForUi));

            if (model == null)
                throw new ArgumentNullException(nameof(model));

            UpgradeHandlersRepository = CreateRepository();
            InventoryContext = CreateInventoryContext(placeForUi, model);
            ShedView = LoadView(placeForUi);
        }

        private UpgradeHandlersRepository CreateRepository()
        {
            UpgradeItemConfig[] upgradeConfigs = ContentDataSourceLoader.LoadUpgradeItemConfigs(_dataSourcePath);
            var repository = new UpgradeHandlersRepository(upgradeConfigs);
            AddRepository(repository);

            return repository;
        }

        private ShedView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_viewPath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<ShedView>();
        }

        private InventoryContext CreateInventoryContext(Transform placeForUi, IInventoryModel model)
        {
            var context = new InventoryContext(placeForUi, model);
            AddContext(context);

            return context;
        }
    }
}